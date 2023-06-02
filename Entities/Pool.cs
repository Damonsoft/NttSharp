using NttSharp.Collections;
using NttSharp.Logic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace NttSharp.Entities
{
    public struct Pool
    {
        public bool HasAny => Map.DenseLength is not 0;

        public Type NetType;
        public int TypeID;
        public int ItemSize;
        internal ChunkedBytes Bytes;
        internal SparseSet Map;

        private Pool(Type type, int typeID, int step, SparseSet set, ChunkedBytes bytes)
        {
            this.NetType = type;
            this.TypeID = typeID;
            this.ItemSize = step;
            this.Bytes = bytes;
            this.Map = set;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Contains(int entity) => Map.Contains(entity);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly int GetDenseLength() => Map.DenseLength;

        public unsafe void AddComponent<T>(int entity, in T value) where T : unmanaged
        {
            Debug.Assert(typeof(T).IsEquivalentTo(NetType), $"Requested component type must be of type {typeof(T).FullName}");

            // Check if the set contains
            // the value already.
            if (!Map.Contains(entity))
            {
                // Add a new bi-directional
                // reference to an entity.
                int offset = ChunkedBytes.Ensure<T>(ref Bytes, SparseSet.Add(ref Map, entity));

                // Write the new value into the pool.
                Bytes.Write(offset, in value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref T GetComponent<T>(int entity) where T : unmanaged
        {
            Debug.Assert(typeof(T).IsEquivalentTo(NetType), $"Requested component type must be of type {typeof(T).FullName}");

            return ref Component.GetComponent<T>(entity, in Map, in Bytes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly void SetComponent<T>(int entity, in T value) where T : unmanaged
        {
            Debug.Assert(typeof(T).IsEquivalentTo(NetType), $"Requested component type must be of type {typeof(T).FullName}");

            Component.SetComponent(entity, in value, in Map, in Bytes);
        }

        public readonly void RemoveComponent(int entity)
        {
            if (Map.Contains(entity))
            {
                int offset = Map.GetSparse(entity);
                int length = Map.DenseLength - offset - 1;

                for (int i = 0; i < length; i++)
                {
                    int source = offset + i + 1;
                    int target = offset + i;
                    Bytes.CopyWithin(source, target, ItemSize);
                }
                SparseSet.Remove(Map, entity);
            }
        }

        public static unsafe Pool Create<T>(int capacity) where T : unmanaged
        {
            return new Pool(typeof(T), TypeID<T>.Unique, sizeof(T), SparseSet.Create(capacity), ChunkedBytes.Create(capacity * sizeof(T)));
        }
    }
}
