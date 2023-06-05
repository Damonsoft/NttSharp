using NttSharp.Collections;
using NttSharp.Logic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace NttSharp.Entities
{
    public struct Pool
    {
        public bool HasAny => Set.DenseLength is not 0;

        public Type NetType;
        public int TypeID;
        public int ItemSize;
        internal SparseSet Set;
        internal ChunkedBytes Bytes;

        private Pool(Type type, int typeID, int step, SparseSet set, ChunkedBytes bytes)
        {
            this.NetType = type;
            this.TypeID = typeID;
            this.ItemSize = step;
            this.Bytes = bytes;
            this.Set = set;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Contains(ntt entity) => Set.Contains(entity);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly int GetDenseLength() => (int)Set.DenseLength;

        public void AddComponent<T>(ntt entity, in T value) where T : unmanaged
        {
            Debug.Assert(typeof(T).IsEquivalentTo(NetType), $"Requested component type must be of type {typeof(T).FullName}");

            // Check if the set contains
            // the value already.
            if (Set.Contains(entity) is false)
            {
                // Add the entity to the sparse set. (Returns the index).
                // Then use the index to ensure we have enough bytes allocated
                // to consume the incoming component. (Returns the index).
                // Then  use the index again to write the component into
                // the byte buffer.
                Bytes.Write(ChunkedBytes.Ensure<T>(ref Bytes, (int)SparseSet.Add(ref Set, entity)), in value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref T GetComponent<T>(ntt entity) where T : unmanaged
        {
            Debug.Assert(typeof(T).IsEquivalentTo(NetType), $"Requested component type must be of type {typeof(T).FullName}");

            return ref Component.GetComponent<T>(entity, in Set, in Bytes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly void SetComponent<T>(ntt entity, in T value) where T : unmanaged
        {
            Debug.Assert(typeof(T).IsEquivalentTo(NetType), $"Requested component type must be of type {typeof(T).FullName}");

            Component.SetComponent(entity, in value, in Set, in Bytes);
        }

        public void RemoveComponent(ntt entity)
        {
            if (Set.Contains(entity))
            {
                ntt offset = Set.GetSparse(entity);
                ntt length = Set.DenseLength - offset - 1;

                for (ntt i = 0; i < length; i++)
                {
                    Bytes.CopyWithin((int)(offset + i + 1), (int)(offset + i), ItemSize);
                }
                SparseSet.Remove(ref Set, entity);
            }
        }

        public static unsafe Pool Create<T>(int capacity) where T : unmanaged
        {
            return new Pool(typeof(T), TypeID<T>.Unique, sizeof(T), SparseSet.Create(capacity), ChunkedBytes.Create(capacity * sizeof(T)));
        }
    }
}
