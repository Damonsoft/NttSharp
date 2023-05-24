using NttSharp.Entities;
using NttSharp.Extensions;
using NttSharp.Logic;

namespace NttSharp.Models
{
    public struct ReverseView_Enumerator1
    {
        public readonly int Current => value;

        int index;
        int value;
        Pool pool_a;

        public ReverseView_Enumerator1(int index, Pool pool_a)
        {
            this.index = 0;
            this.value = 0;
            this.pool_a = pool_a;
        }

        public unsafe bool MoveNext()
        {
            int* ptr_a = PoolEx.GetBody(pool_a);

            while (index < Dense.GetLength(ptr_a))
            {
                value = Dense.GetUnchecked(ptr_a, index++);

                return true;
            }
            return false;
        }
    }

    public struct ReverseView_Enumerator2
    {
        public readonly int Current => value;

        int index;
        int value;
        Pool pool_a;
        Pool pool_b;

        public ReverseView_Enumerator2(int index, Pool pool_a, Pool pool_b)
        {
            this.index = index;
            this.value = 0;
            this.pool_a = pool_a;
            this.pool_b = pool_b;
        }

        public unsafe bool MoveNext()
        {
            int* ptr_a = PoolEx.GetBody(pool_a);
            int* ptr_b = PoolEx.GetBody(pool_b);

            while (index < Dense.GetLength(ptr_a))
            {
                value = Dense.GetUnchecked(ptr_a, index++);

                if (Sparse.Contains(ptr_b, value))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public struct ReverseView_Enumerator3
    {
        public readonly int Current => value;

        int index;
        int value;
        Pool pool_a;
        Pool pool_b;
        Pool pool_c;

        public ReverseView_Enumerator3(int index, Pool pool_a, Pool pool_b, Pool pool_c)
        {
            this.index = index;
            this.value = 0;
            this.pool_a = pool_a;
            this.pool_b = pool_b;
            this.pool_c = pool_c;
        }

        public unsafe bool MoveNext()
        {
            int* ptr_a = PoolEx.GetBody(pool_a);
            int* ptr_b = PoolEx.GetBody(pool_b);
            int* ptr_c = PoolEx.GetBody(pool_c);

            while (index < Dense.GetLength(ptr_a))
            {
                value = Dense.GetUnchecked(ptr_a, index++);

                if (Sparse.Contains(ptr_b, value) &&
                    Sparse.Contains(ptr_c, value))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public struct ReverseView_Enumerator4
    {
        public readonly int Current => value;

        int index;
        int value;
        Pool pool_a;
        Pool pool_b;
        Pool pool_c;
        Pool pool_d;

        public ReverseView_Enumerator4(int index, Pool pool_T, Pool pool_U, Pool pool_X, Pool pool_Y)
        {
            this.index = index;
            this.value = 0;
            this.pool_a = pool_T;
            this.pool_b = pool_U;
            this.pool_c = pool_X;
            this.pool_d = pool_Y;
        }

        public unsafe bool MoveNext()
        {
            int* ptr_a = PoolEx.GetBody(pool_a);
            int* ptr_b = PoolEx.GetBody(pool_b);
            int* ptr_c = PoolEx.GetBody(pool_c);
            int* ptr_d = PoolEx.GetBody(pool_d);

            while (index < Dense.GetLength(ptr_a))
            {
                value = Dense.GetUnchecked(ptr_a, index++);

                if (Sparse.Contains(ptr_b, value) &&
                    Sparse.Contains(ptr_c, value) &&
                    Sparse.Contains(ptr_d, value))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public readonly unsafe struct ReverseView<A> where A : unmanaged
    {
        readonly Pool pool_a;

        public ReverseView(Pool pool_T)
        {
            this.pool_a = pool_T;
        }

        public readonly ref A Get1(int entity) => ref Component.GetComponent(entity, pool_a.Set, pool_a.Count, (A*)pool_a.BytePtr);

        public View_Enumerator1 GetEnumerator() => new View_Enumerator1(0, pool_a);
    }

    public readonly unsafe struct ReverseView<A, B>
        where A : unmanaged
        where B : unmanaged
    {
        readonly Pool pool_a;
        readonly Pool pool_b;

        public ReverseView(
            Pool pool_a,
            Pool pool_b)
        {
            this.pool_a = pool_a;
            this.pool_b = pool_b;
        }

        public readonly ref A Get1(int entity) => ref Component.GetComponent(entity, pool_a.Set, pool_a.Count, (A*)pool_a.BytePtr);
        public readonly ref B Get2(int entity) => ref Component.GetComponent(entity, pool_b.Set, pool_b.Count, (B*)pool_b.BytePtr);

        public View_Enumerator2 GetEnumerator() => new View_Enumerator2(0, pool_a, pool_b);
    }

    public readonly unsafe struct ReverseView<A, B, C>
        where A : unmanaged
        where B : unmanaged
        where C : unmanaged
    {
        readonly Pool pool_a;
        readonly Pool pool_b;
        readonly Pool pool_c;

        public ReverseView(
            Pool pool_a,
            Pool pool_b,
            Pool pool_c)
        {
            this.pool_a = pool_a;
            this.pool_b = pool_b;
            this.pool_c = pool_c;
        }

        public readonly ref A Get1(int entity) => ref Component.GetComponent(entity, pool_a.Set, pool_a.Count, (A*)pool_a.BytePtr);
        public readonly ref B Get2(int entity) => ref Component.GetComponent(entity, pool_b.Set, pool_b.Count, (B*)pool_b.BytePtr);
        public readonly ref C Get3(int entity) => ref Component.GetComponent(entity, pool_c.Set, pool_c.Count, (C*)pool_c.BytePtr);
        public View_Enumerator3 GetEnumerator() => new View_Enumerator3(0, pool_a, pool_b, pool_c);
    }

    public readonly unsafe struct ReverseView<A, B, C, D>
        where A : unmanaged
        where B : unmanaged
        where C : unmanaged
        where D : unmanaged
    {
        readonly Pool pool_a;
        readonly Pool pool_b;
        readonly Pool pool_c;
        readonly Pool pool_d;

        public ReverseView(
            Pool pool_a,
            Pool pool_b,
            Pool pool_c,
            Pool pool_d)
        {
            this.pool_a = pool_a;
            this.pool_b = pool_b;
            this.pool_c = pool_c;
            this.pool_d = pool_d;
        }

        public readonly ref A Get1(int entity) => ref Component.GetComponent(entity, pool_a.Set, pool_a.Count, (A*)pool_a.BytePtr);
        public readonly ref B Get2(int entity) => ref Component.GetComponent(entity, pool_b.Set, pool_b.Count, (B*)pool_b.BytePtr);
        public readonly ref C Get3(int entity) => ref Component.GetComponent(entity, pool_c.Set, pool_c.Count, (C*)pool_c.BytePtr);
        public readonly ref D Get4(int entity) => ref Component.GetComponent(entity, pool_d.Set, pool_d.Count, (D*)pool_d.BytePtr);

        public View_Enumerator4 GetEnumerator() => new View_Enumerator4(0, pool_a, pool_b, pool_c, pool_d);
    }
}
