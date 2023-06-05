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

        internal readonly byte** Bytes;
        internal readonly int Count;

        public ChunkedBytes(byte** Bytes, int count)
        {
            this.Bytes = Bytes;
            this.Count = count;
        }

        public readonly int CountOf<T>() where T : unmanaged => (CHUNK_TOTAL_SIZE / sizeof(T)) * Count;

        public readonly void CopyWithin(int x, int y, int size)
        {
            Buffer.MemoryCopy(
                &Bytes[x / (CHUNK_TOTAL_SIZE / size)][x % (CHUNK_TOTAL_SIZE / size) * size],
                &Bytes[y / (CHUNK_TOTAL_SIZE / size)][y % (CHUNK_TOTAL_SIZE / size) * size],
                size,
                size);
        }

        public readonly void Write<T>(long index, in T value) where T : unmanaged
        {
            long x = index / (CHUNK_TOTAL_SIZE / sizeof(T));
            long y = index % (CHUNK_TOTAL_SIZE / sizeof(T));
            ((T*)Bytes[x])[y] = value;
        }

        public readonly ref T Ref<T>(long index) where T : unmanaged
        {
            long x = index / (CHUNK_TOTAL_SIZE / sizeof(T));
            long y = index % (CHUNK_TOTAL_SIZE / sizeof(T));
            return ref ((T*)Bytes[x])[y];
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

            byte** _internal = (byte**)Native.Resize(new IntPtr(bytes.Bytes), new_count * sizeof(byte*));

            for (int i = old_count; i < old_count + (new_count - old_count); i++)
            {
                _internal[i] = Native.Malloc<byte>(CHUNK_TOTAL_SIZE);
            }
            bytes = new ChunkedBytes(_internal, new_count);
        }

        public static ChunkedBytes Create(int capacity)
        {
            int count = GetCount(capacity);
            byte** _internal = (byte**)Native.Malloc(count * sizeof(byte*));

            for (int i = 0; i < count; i++)
            {
                _internal[i] = Native.Malloc<byte>(CHUNK_TOTAL_SIZE);
            }
            return new ChunkedBytes(_internal, count);
        }

        private static int GetCount(int capacity) => (capacity / CHUNK_TOTAL_SIZE) + (capacity % CHUNK_TOTAL_SIZE != 0 ? 1 : 0);
    }
}