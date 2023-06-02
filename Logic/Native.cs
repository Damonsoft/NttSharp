using NttSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NttSharp.Logic
{
    public static unsafe class Native
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SizeOf<T>(int count) where T : unmanaged
        {
            return sizeof(T) * count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T* Malloc<T>(int count) where T : unmanaged
        {
            return (T*)NativeMemory.Alloc((nuint)SizeOf<T>(count));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void* Malloc(int size)
        {
            return NativeMemory.Alloc((nuint)size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Free<T>(T* pointer) where T : unmanaged
        {
            NativeMemory.Free(pointer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T* Resize<T>(T* pointer, int count) where T : unmanaged
        {
            return (T*)NativeMemory.Realloc(pointer, (nuint)SizeOf<T>(count));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void* Resize(IntPtr pointer, int size)
        {
            return NativeMemory.Realloc(pointer.ToPointer(), (nuint)size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void* Resize(IntPtr pointer, int count, int step)
        {
            return NativeMemory.Realloc(pointer.ToPointer(), (nuint)(count * step));
        }
    }
}
