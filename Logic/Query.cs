using NttSharp.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NttSharp.Logic
{
    public static class Query
    {
        public const long QUERY_NIL = ((long)int.MaxValue << 32) + (long)int.MaxValue;

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static bool Contains(long query, int entity) => (int)(query) == entity;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ReadSparse(long query) => (int)(query >> 32);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ReadDense(long query) => (int)(query);

        public unsafe static long Do(int* body, int index)
        {
            if (index < body[-1])
            {
                long dense_index = body[index * 2 + 1];

                if (dense_index < body[-2])
                {
                    long sparse_index = body[dense_index * 2];

                    return (dense_index << 32) + sparse_index;
                }
            }
            return Logic.Query.QUERY_NIL;
        }

        public unsafe static bool Do(int* body, int index, out long query)
        {
            if (index < body[-1])
            {
                long dense_index = body[index * 2 + 1];

                if (dense_index < body[-2])
                {
                    long sparse_index = body[dense_index * 2];

                    query = (dense_index << 32) + sparse_index;

                    return sparse_index == index;
                }
            }
            query = Logic.Query.QUERY_NIL;

            return false;
        }
    }
}
