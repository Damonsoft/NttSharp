using System.Diagnostics;
using System.Runtime.CompilerServices;
using NttSharp.Collections;

namespace NttSharp.Logic
{
    public static unsafe class Component
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetComponent<T>(int entity, in T value, in SparseSet set, in ChunkedBytes bytes) where T : unmanaged
        {
#if DEBUG
            Debug.Assert(set.Contains(entity), "Set already contain the entity!");
#endif
            bytes.Write(set.GetSparse(entity), in value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T GetComponent<T>(int entity, scoped in SparseSet set, scoped in ChunkedBytes bytes) where T : unmanaged
        {
#if DEBUG
            Debug.Assert(set.Contains(entity), "Set doesn't contain the entity!");
#endif
            return ref bytes.Ref<T>(set.GetSparse(entity));
        }
    }
}
