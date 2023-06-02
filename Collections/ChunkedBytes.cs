using NttSharp.Logic;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace NttSharp.Collections
{
    public readonly unsafe struct ChunkedBytes
    {
        public const int CHUNK_TOTAL_SIZE = 65_536;

        public readonly int Capacity => Count * CHUNK_TOTAL_SIZE;

        private readonly byte** _Internal;
        private readonly int Count;

        public ChunkedBytes(byte** @internal, int count)
        {
            _Internal = @internal;
            Count = count;
        }

        public readonly int CountOf<T>() where T : unmanaged => (CHUNK_TOTAL_SIZE / sizeof(T)) * Count;


        public readonly void CopyWithin(int x, int y, int size)
        {
            int _cx = x / (CHUNK_TOTAL_SIZE / size);
            int _ox = x % (CHUNK_TOTAL_SIZE / size);

            int _cy = y / (CHUNK_TOTAL_SIZE / size);
            int _oy = y % (CHUNK_TOTAL_SIZE / size);

            //Console.WriteLine($"{index}: {_c}: {_o * sizeof(T)}");

            byte* _src_0 = _Internal[_cx];
            byte* _dst_0 = _Internal[_cy];

            Buffer.MemoryCopy(
                (void*)&_src_0[_ox * size],
                (void*)&_dst_0[_oy * size],
                size,
                size);
        }

        public readonly void Write<T>(int index, in T value) where T : unmanaged
        {
            int _c = index / (CHUNK_TOTAL_SIZE / sizeof(T));
            int _o = index % (CHUNK_TOTAL_SIZE / sizeof(T));

            //Console.WriteLine($"{index}: {_c}: {_o * sizeof(T)}");

            T* chunk = (T*)_Internal[_c];

            chunk[_o] = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref T Ref<T>(int index) where T : unmanaged
        {
            return ref ((T*)_Internal[index / (CHUNK_TOTAL_SIZE / sizeof(T))])[index % (CHUNK_TOTAL_SIZE / sizeof(T))];
        }

        public static int Ensure<T>(ref ChunkedBytes bytes, int count) where T : unmanaged
        {
            if (count >= bytes.CountOf<T>())
            {
                Resize(ref bytes, (count + 1) * sizeof(T));
            }
            return count;
        }

        public static void Resize(ref ChunkedBytes bytes, int capacity)
        {
            int old_count = bytes.Count;
            int new_count = GetCount(capacity);

            byte** _internal = (byte**)Native.Resize(new IntPtr(bytes._Internal), new_count * sizeof(byte*));

            for(int i = old_count; i < old_count + (new_count - old_count); i++)
            {
                _internal[i] = Native.Malloc<byte>(CHUNK_TOTAL_SIZE);
            }
            bytes = new ChunkedBytes(_internal, new_count);
        }

        public static ChunkedBytes Create(int capacity)
        {
            int count = GetCount(capacity);
            byte** _internal = (byte**)Native.Malloc(count * sizeof(byte*));

            for(int i = 0; i <  count; i++)
            {
                _internal[i] = Native.Malloc<byte>(CHUNK_TOTAL_SIZE);
            }
            return new ChunkedBytes(_internal, count);
        }

        private static int GetCount(int capacity) => (capacity / CHUNK_TOTAL_SIZE) + (capacity % CHUNK_TOTAL_SIZE != 0 ? 1 : 0);

        private static int GetOffset(int offset, int size) => (offset / size) % (CHUNK_TOTAL_SIZE / size);  
    }
}
