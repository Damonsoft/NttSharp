using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace NttSharp.Logic
{
    public static unsafe partial class Sparse
    {
        public const int ROW_LENGTH = 2;
        public const int SPARSE_OFFSET = 1;
        public const int DENSE_OFFSET = 0;
        public const int HEADER_LENGTH = 2;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Init(ntt[] data)
        {
            // Write the length of the 
            // array to the header and 
            // zero the dense length.
            data[0] = 0;
            data[1] = (data.Length - HEADER_LENGTH) / ROW_LENGTH;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ntt GetLength(ntt[] data) => data[1];


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ntt Add(ref ntt[] data, ntt offset)
        {
            return Poke(ref data, offset, Dense.Add(ref data, offset));
        }

        public static void Remove(ntt[] data, ntt index)
        {
            if (Contains(data, index))
            {
                ntt start = Sparse.Peek(data, index);
                ntt length = Dense.PeekLength(data) - start - 1;

                Dense.PokeUnsafe(data, start, 0);
                Sparse.PokeUnsafe(data, index, 0);

                for (int i = 0; i < length; i++)
                {
                    ntt next = Dense.Peek(data, start + i + 1);

                    Sparse.PokeUnsafe(data, next, start + i);
                    Dense.PokeUnsafe(data, start + i, next);
                }
                var old_length = Dense.PeekLength(data); ;

                Dense.PokeUnsafe(data, old_length - 1, 0);
                Dense.PokeLength(data, old_length - 1);

                return;
            }
            throw new Exception($"Sparse and dense sets don't match at offset {index}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ntt Peek(ntt[] data, ntt offset)
        {
            return data[(offset + 1) * ROW_LENGTH + SPARSE_OFFSET];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ntt Poke(ntt[] data, ntt offset, ntt value)
        {
            return data[(offset + 1) * ROW_LENGTH + SPARSE_OFFSET] = value;
        }

        public static ntt Poke(ref ntt[] set, ntt offset, ntt value)
        {
            if (offset >= GetLength(set))
            {
                ResizeSet(ref set, (int)(offset * 2));

                return Poke(ref set, offset, value);
            }
            Debug.Assert(offset < set.Length);

            return Poke(set, offset, value);
        }

        public static ntt PokeUnsafe(ntt[] set, ntt offset, ntt value)
        {
            Debug.Assert(offset < set.Length);

            return Poke(set, offset, value);
        }

        public static int ResizeSet(ref ntt[] data, int size)
        {
            Helpers.ResizeArray(ref data, (size + HEADER_LENGTH) * ROW_LENGTH, true);

            // Apply the new size to the header
            // of the array while preserving
            // the length of the dense set.
            return (int)Poke(data, -1, size);
        }

        public static bool Contains(ntt[] ptr, ntt index)
        {
            // Check that the index
            // is within the bounds of the
            // sparse set.
            if (index >= ptr[1]) return false;

            // Get the dense index from
            // the sparse set.
            ntt d_i = ptr[(index + 1) * ROW_LENGTH + SPARSE_OFFSET];

            // Check that the dense
            // index is within the bounds
            // of the dense set.
            if (d_i >= ptr[0]) return false;

            // Compare the value from
            // the dense set with the
            // index requested AKA
            // dense[sparse[index]] == index
            return ptr[(d_i + 1) * ROW_LENGTH] == index;
        }

        public static unsafe bool Contains(ntt* ptr, ntt entity)
        {
            int s_len = ptr[-1];
            int d_len = ptr[-2];

            // Check that the index
            // is within the bounds of the
            // sparse set.
            if (entity >= s_len) return false;

            // Get the dense index from
            // the sparse set.
            ntt d_i = ptr[entity * ROW_LENGTH + SPARSE_OFFSET];

            // Check that the dense
            // index valid;
            if (d_i >= d_len) return false;

            // Compare the value from
            // the dense set with the
            // index requested AKA
            // dense[sparse[index]] == index
            return ptr[d_i * ROW_LENGTH] == entity;
        }

        public static unsafe bool Contains(ntt* ptr, ntt entity, int s_len, int d_len)
        {
            // Check that the index
            // is within the bounds of the
            // sparse set.
            if (entity >= s_len) return false;

            // Get the dense index from
            // the sparse set.
            ntt d_i = ptr[entity * ROW_LENGTH + SPARSE_OFFSET];

            // Check that the dense
            // index valid;
            if (d_i >= d_len) return false;

            // Compare the value from
            // the dense set with the
            // index requested AKA
            // dense[sparse[index]] == index
            return ptr[d_i * ROW_LENGTH] == entity;
        }
    }
}