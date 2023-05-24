using NttSharp.Collections;
using NttSharp.Entities;
using NttSharp.Logic;
using System.Runtime.CompilerServices;

namespace NttSharp.Extensions
{
    // To Do: Pool converted to a class
    // move functions into pool as methods
    public static unsafe class PoolEx
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int* GetBody(Pool source) => source.Set.Body;

        public static void RemoveComponent(Pool pool, int entity)
        {
            SparseSet set = pool.Set;

            if (set.Contains(entity))
            {
                int offset = set.GetSparse(entity);
                int length = set.DenseLength - offset - 1;

                for (int i = 0; i < length; i++)
                {
                    int src = set.GetDense(offset + i + 1);
                    int dst = set.GetDense(offset + i);

                    PoolEx.WriteComponent(pool, dst, pool.Step, PoolEx.GetComponent(pool, src));
                }
                SparseSet.Remove(set, entity);
            }
        }

        public static void WriteComponent<T>(Pool source, int entity, in T component) where T : unmanaged
        {
            SparseSet set = source.Set;

            if (set.Contains(entity))
            {
                int index = set.GetSparse(entity);

                ((T*)source.BytePtr)[index] = component;
            }
        }

        public static void WriteComponent(Pool pool, int entity, int step, IntPtr source)
        {
            SparseSet set = pool.Set;

            if (set.Contains(entity))
            {
                int index = set.GetSparse(entity);

                Buffer.MemoryCopy(
                    source.ToPointer(),
                    Unsafe.AsPointer(ref pool.BytePtr[index * step]),
                    step,
                    step);
            }
        }

        public static void AddComponent<T>(Pool source, int entity, T value) where T : unmanaged
        {
            ref SparseSet set = ref source.Set;

            // Check if the set contains
            // the value already.
            if (set.Contains(entity) is false)
            {
                // Add a new bi-directional
                // reference to an entity.
                int index = SparseSet.Add(ref set, entity);

                // Check if the length of
                // the components is longer
                // than our buffer.
                if (index * sizeof(T) >= source.Bytes.Length)
                {
                    // If so resize our component
                    // buffer to the length * 2;
                    PoolEx.ResizeBuffer(source, index * 2);
                }
                // Write the new value into
                // the pool.
                SetComponent<T>(source, entity, in value);
            }
        }

        public static void ResizeBuffer(Pool source, int length)
        {
            source.Count = length;
            source.Bytes = Helpers.ResizeArray(ref source.Bytes, length * source.Step, true);
            source.BytePtr = (byte*)Unsafe.AsPointer(ref source.Bytes[0]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntPtr GetComponent(Pool source, int entity)
        {
            return Component.GetComponent(source.Set, entity, source.Bytes, source.Step);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T GetComponent<T>(Pool source, int entity) where T : unmanaged
        {
            return ref Component.GetComponent<T>(source.Set, entity, source.Bytes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetComponent<T>(Pool source, int entity, in T value) where T : unmanaged
        {
            Component.SetComponent<T>(source.Set, entity, source.Bytes, in value);
        }
    }
}
