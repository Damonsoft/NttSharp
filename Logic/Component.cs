using System.Runtime.CompilerServices;
using NttSharp.Collections;

namespace NttSharp.Logic
{
    public static unsafe class Component
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntPtr GetComponent(SparseSet set, int entity, byte[] bytes, int step)
        {
            return new IntPtr(Unsafe.AsPointer(ref bytes[set.GetSparse(entity) * step]));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T GetComponent<T>(SparseSet set, int entity, byte[] bytes) where T : unmanaged
        {
            return ref ((T*)Unsafe.AsPointer(ref bytes[0]))[set.GetSparse(entity)];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetComponent<T>(SparseSet set, int entity, byte[] bytes, in T value) where T : unmanaged
        {
            ((T*)Unsafe.AsPointer(ref bytes[0]))[set.GetSparse(entity)] = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T GetComponent<T>(int entity, SparseSet set, int count, T* bytes) where T : unmanaged
        {
            int index = set.GetSparse(entity);

            if (index < count)
            {
                return ref bytes[index];
            }
            return ref Unsafe.AsRef<T>((void*)0);
        }
    }
}
