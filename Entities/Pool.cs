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
            Debug.Assert(typeof(T).IsEquivalentTo(NetType), "Requested component type doesn't match the pools!");

            ref SparseSet map = ref Map;

            // Check if the set contains
            // the value already.
            if (!map.Contains(entity))
            {
                // Add a new bi-directional
                // reference to an entity.
                int offset = SparseSet.Add(ref map, entity) * sizeof(T);

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
            Debug.Assert(typeof(T).IsEquivalentTo(NetType), "Requested component type doesn't match the pools!");

            return ref Component.GetComponent<T>(entity, in Map, in Bytes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetComponent<T>(int entity, in T value) where T : unmanaged
        {
            Debug.Assert(typeof(T).IsEquivalentTo(NetType), "Requested component type doesn't match the pools!");

            Component.SetComponent(entity, in value, in Map, in Bytes);
        }

        public void RemoveComponent(int entity)
        {
            ref SparseSet set = ref Map;

            if (set.Contains(entity))
            {
                int offset = set.GetSparse(entity);
                int length = set.DenseLength - offset - 1;

                for (int i = 0; i < length; i++)
                {
                    int source = offset + i + 1;
                    int target = offset + i;
                    Bytes.CopyWithin(source * ItemSize, target * ItemSize, ItemSize);
                }
                SparseSet.Remove(set, entity);
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
