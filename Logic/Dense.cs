using System.Runtime.CompilerServices;

namespace NttSharp.Logic
{
    public static class Dense
    {
        public static int Get(int[] data, int offset)
        {
            int length = data[0];

            if (offset < length)
            {
                return data[(offset + 1) * 2];
            }
            throw new IndexOutOfRangeException();
        }

        public static unsafe int Get(int* data, int offset)
        {
            if (offset < data[-2])
            {
                return data[offset * 2];
            }
            throw new IndexOutOfRangeException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SetUnsafe(int[] data, int offset, int value)
        {
            return data[(offset + 1) * 2] = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SetLength(int[] data, int value)
        {
            return data[0] = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int GetUnchecked(int* data, int offset) => data[offset * 2];

        public static int Add(ref int[] data, int value)
        {
            //
            int slot = GetLength(data);
            int size = GetLength(data) + 1;

            //
            if (size >= Sparse.GetLength(data))
            {
                //
                Sparse.ResizeSet(ref data, (size + 1) * 2);

                //
                return Add(ref data, value);
            }
            //
            SetLength(data, size);
            //
            SetUnsafe(data, slot, value);
            //
            return slot;

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetLength(int[] data) => data[0];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int GetLength(int* data) => data[-2];
    }
}
