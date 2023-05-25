using NttSharp.Collections;
using NttSharp.Logic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NttSharp.Entities
{
    public sealed class Pool
    {
        public bool HasAny => Map.DenseLength is not 0;

        public readonly Type NetType;
        public readonly int TypeID;
        public readonly int ItemSize;
        internal NativeBytes Bytes;
        internal SparseSet Map;

        private Pool(Type type, int typeID, int step, SparseSet set, NativeBytes bytes)
        {
            this.NetType = type;
            this.TypeID = typeID;
            this.ItemSize = step;
            this.Bytes = bytes;
            this.Map = set;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(int entity) => Map.Contains(entity);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetDenseLength() => Map.DenseLength;

        public unsafe void AddComponent<T>(int entity, in T value) where T : unmanaged
        {
            Debug.Assert(typeof(T).IsEquivalentTo(NetType), $"Requested component type must be of type {typeof(T).FullName}");

            // Check if the set contains
            // the value already.
            if (!Map.Contains(entity))
            {
                // Add a new bi-directional
                // reference to an entity.
                int offset = SparseSet.Add(ref Map, entity) * sizeof(T);

                // Check if the length of
                // the components is longer
                // than our buffer.
                if ((offset + sizeof(T)) >= Bytes.Capacity)
                {
                    // If it is then resize
                    // our buffer two times
                    // the requested length.
                    NativeBytes.Resize(ref Bytes, offset * 2);
                }
                // Write the new value into the pool.
                Bytes.Write(offset, in value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T GetComponent<T>(int entity) where T : unmanaged
        {
            Debug.Assert(typeof(T).IsEquivalentTo(NetType), $"Requested component type must be of type {typeof(T).FullName}");

            return ref Component.GetComponent<T>(entity, in Map, in Bytes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetComponent<T>(int entity, in T value) where T : unmanaged
        {
            Debug.Assert(typeof(T).IsEquivalentTo(NetType), $"Requested component type must be of type {typeof(T).FullName}");

            Component.SetComponent(entity, in value, in Map, in Bytes);
        }

        public void RemoveComponent(int entity)
        {
            if (Map.Contains(entity))
            {
                int offset = Map.GetSparse(entity);
                int length = Map.DenseLength - offset - 1;

                for (int i = 0; i < length; i++)
                {
                    int source = offset + i + 1;
                    int target = offset + i;
                    Bytes.CopyWithin(source * ItemSize, target * ItemSize, ItemSize);
                }
                SparseSet.Remove(Map, entity);
            }
        }

        public static unsafe Pool Create<T>(int capacity) where T : unmanaged
        {
            return new Pool(typeof(T), TypeID<T>.Unique, sizeof(T), SparseSet.Create(capacity), Collections.NativeBytes.Allocate(capacity * sizeof(T)));
        }

        public static unsafe Pool Create(TypeDetails details, int capacity)
        {
            return new Pool(details.Handle, details.Unique, details.Size, SparseSet.Create(capacity), Collections.NativeBytes.Allocate(capacity * details.Size)); ;
        }
    }
}
