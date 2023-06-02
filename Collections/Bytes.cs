using NttSharp.Logic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace NttSharp.Collections
{
    public unsafe readonly struct Bytes
    {
        public readonly int Capacity => Reference.Length;

        internal readonly byte* Pointer;
        internal readonly byte[] Reference;

        private Bytes(byte* pointer, byte[] reference)
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

        public static Bytes Allocate(int length)
        {
            byte[] bytes = GC.AllocateArray<byte>(length, pinned: true);

            return new Bytes((byte*)Unsafe.AsPointer(ref bytes[0]), bytes);
        }

        public static void Resize(ref Bytes bytes, int size)
        {
            byte[] temp = Helpers.ResizeArray(bytes.Reference, size, pinned: true);

            bytes = new Bytes((byte*)Unsafe.AsPointer(ref temp[0]), temp);
        }
    }
}
