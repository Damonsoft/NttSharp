using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace NttSharp.Logic
{
    public static unsafe partial class Sparse
    {
        public static int[] Create(int capacity, bool pinned)
        {
            int[] data = GC.AllocateArray<int>(capacity, pinned);
            Init(data);
            return data;
        }

        public static void Init(Span<int> data)
        {
            // Write the length of the 
            // array to the header and 
            // zero the dense length.
            data[1] = (data.Length - 2) / 2;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Add(ref int[] data, int offset)
        {
            //
            int dense = Dense.Add(ref data, offset);
            //
            Set(ref data, offset, dense);
            //
            return dense;
        }

        public static void Remove(int[] data, int index)
        {
            if (Contains(data, index))
            {
                int start = Get(data, index);
                int length = Dense.GetLength(data) - start - 1;

                for (int i = 0; i < length; i++)
                {
                    int next = Dense.Get(data, start + i + 1);

                    SetUnsafe(data, next, start + i);
                    Dense.SetUnsafe(data, start + i, next);
                }
                int old_length = Dense.GetLength(data); ;

                SetUnsafe(data, index, 0);
                Dense.SetUnsafe(data, old_length - 1, 0);

                Dense.SetLength(data, old_length - 1);

                return;
            }
            throw new Exception($"Sparse and dense sets don't match at offset {index}");
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Get(int[] data, int offset)
        {
            return data[(offset + 1) * 2 + 1];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Write(int[] data, int offset, int value)
        {
            return data[(offset + 1) * 2 + 1] = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetLength(int[] data)
        {
            return data[1];
        }

        public static int Set(ref int[] set, int offset, int value)
        {
            if (offset >= GetLength(set))
            {
                ResizeSet(ref set, offset * 2);

                return Set(ref set, offset, value);
            }
            Debug.Assert(offset + 1 < set.Length);

            return Write(set, offset, value);
        }

        public static int Set(int[] set, int offset, int value)
        {
            if (offset >= GetLength(set))
            {
                throw new Exception();
            }
            Debug.Assert(offset + 1 < set.Length);

            return Write(set, offset, value);
        }

        public static int SetUnsafe(int[] set, int offset, int value)
        {
            Debug.Assert(offset + 1 < set.Length);

            return Write(set, offset, value);
        }

        public static int ResizeSet(ref int[] data, int size)
        {
            Helpers.ResizeArray(ref data, (size + 2) * 2, false);

            // Apply the new size to the header
            // of the array while preserving
            // the length of the dense set.
            return Write(data, -1, size);
        }

        public static bool Contains(int[] ptr, int index)
        {
            // Check that the index
            // is within the bounds of the
            // sparse set.
            if (index >= ptr[1]) return false;

            // Get the dense index from
            // the sparse set.
            int d_i = ptr[(index + 1) * 2 + 1];

            // Check that the dense
            // index is within the bounds
            // of the dense set.
            if (d_i >= ptr[0]) return false;

            // Compare the value from
            // the dense set with the
            // index requested AKA
            // dense[sparse[index]] == index
            return ptr[(d_i + 1) * 2] == index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe bool Contains(int* ptr, int index)
        {
            //
            if (index >= ptr[-1]) return false;

            //
            return ptr[ptr[index * 2 + 1] * 2] == index;
        }
    }
}
