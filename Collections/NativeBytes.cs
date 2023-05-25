using NttSharp.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NttSharp.Collections
{
    public unsafe readonly struct NativeBytes
    {
        public readonly int Capacity => Reference.Length;

        internal readonly byte* Pointer;
        internal readonly byte[] Reference;

        private NativeBytes(byte* pointer, byte[] reference)
        {
            Pointer = pointer;
            Reference = reference;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntPtr IntPtr<T>(int offset) where T : unmanaged
        {
            Debug.Assert(offset + sizeof(T) < Reference.Length);

            return new IntPtr((T*)&Pointer[offset]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T* Ptr<T>(int offset) where T : unmanaged
        {
            Debug.Assert(offset + sizeof(T) < Reference.Length);

            return (T*)&Pointer[offset];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T Ref<T>(int offset) where T : unmanaged
        {
            Debug.Assert(offset + sizeof(T) < Reference.Length);

            return ref Unsafe.AsRef<T>(Pointer + offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write<T>(int offset, in T value) where T : unmanaged
        {
            Debug.Assert(offset + sizeof(T) < Reference.Length);

            ((T*)(Pointer + offset))[0] = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Copy(void* source, int target, int length)
        {
            Buffer.MemoryCopy(source, &Pointer[target], Reference.Length - length, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyWithin(int source, int target, int length)
        {
            Debug.Assert(target + length < Reference.Length);

            Buffer.MemoryCopy(&Pointer[source], &Pointer[target], Reference.Length - length, length);
        }

        public static NativeBytes Allocate(int length)
        {
            byte[] bytes = GC.AllocateArray<byte>(length, pinned: true);

            return new NativeBytes((byte*)Unsafe.AsPointer(ref bytes[0]), bytes);
        }

        public static void Resize(ref NativeBytes bytes, int size)
        {
            byte[] temp = Helpers.ResizeArray(bytes.Reference, size, pinned: true);

            bytes = new NativeBytes((byte*)Unsafe.AsPointer(ref temp[0]), temp);
        }
    }
}
