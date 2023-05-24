using NttSharp.Entities;
using NttSharp.Extensions;
using NttSharp.Logic;

namespace NttSharp.Models
{
    public struct View_Enumerator1
    {
        public readonly int Current => value;

        int index;
        int value;
        Pool pool_a;

        public View_Enumerator1(int index, Pool pool_a)
        {
            this.value = 0;
            this.index = pool_a.GetDenseLength() - 1;
            this.pool_a = pool_a;
        }

        public unsafe bool MoveNext()
        {
            int* ptr_T = PoolEx.GetBody(pool_a);

            while (index >= 0)
            {
                value = Dense.GetUnchecked(ptr_T, index--);

                return true;
            }
            return false;
        }
    }

    public struct View_Enumerator2
    {
        public readonly int Current => value;

        int value;
        int index;
        Pool pool_a;
        Pool pool_b;

        public View_Enumerator2(int index, Pool pool_a, Pool pool_b)
        {
            this.value = 0;
            this.index = pool_a.GetDenseLength() - 1;
            this.pool_a = pool_a;
            this.pool_b = pool_b;
        }

        public unsafe bool MoveNext()
        {
            int* ptr_a = PoolEx.GetBody(pool_a);
            int* ptr_b = PoolEx.GetBody(pool_b);

            while (index >= 0)
            {
                value = Dense.GetUnchecked(ptr_a, index--);

                if (Sparse.Contains(ptr_b, value))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public struct View_Enumerator3
    {
        public readonly int Current => value;

        int value;
        int index;
        Pool pool_a;
        Pool pool_b;
        Pool pool_c;

        public View_Enumerator3(int index, Pool pool_a, Pool pool_b, Pool pool_c)
        {
            this.value = 0;
            this.index = pool_a.GetDenseLength() - 1;
            this.pool_a = pool_a;
            this.pool_b = pool_b;
            this.pool_c = pool_c;
        }

        public unsafe bool MoveNext()
        {
            int* ptr_a = PoolEx.GetBody(pool_a);
            int* ptr_b = PoolEx.GetBody(pool_b);
            int* ptr_c = PoolEx.GetBody(pool_c);

            while (index >= 0)
            {
                value = Dense.GetUnchecked(ptr_a, index--);

                if (Sparse.Contains(ptr_b, value) &&
                    Sparse.Contains(ptr_c, value))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public struct View_Enumerator4
    {
        public readonly int Current => value;

        int value;
        int index;
        Pool pool_a;
        Pool pool_b;
        Pool pool_c;
        Pool pool_d;

        public View_Enumerator4(int index, Pool pool_a, Pool pool_b, Pool pool_c, Pool pool_d)
        {
            this.value = 0;
            this.index = pool_a.GetDenseLength() - 1;
            this.pool_a = pool_a;
            this.pool_b = pool_b;
            this.pool_c = pool_c;
            this.pool_d = pool_d;
        }

        public unsafe bool MoveNext()
        {
            int* ptr_a = PoolEx.GetBody(pool_a);
            int* ptr_b = PoolEx.GetBody(pool_b);
            int* ptr_c = PoolEx.GetBody(pool_c);
            int* ptr_d = PoolEx.GetBody(pool_d);

            while (index >= 0)
            {
                value = Dense.GetUnchecked(ptr_a, index--);

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

    public readonly unsafe struct View<A>
        where A : unmanaged
    {
        readonly Pool pool_a;

        public View(Pool pool_a)
        {
            this.pool_a = pool_a;
        }

        public readonly ref A Get1(int entity) => ref Component.GetComponent(entity, pool_a.Set, pool_a.Count, (A*)pool_a.BytePtr);
        public ReverseView_Enumerator1 GetEnumerator() => new ReverseView_Enumerator1(0, pool_a);
    }

    public readonly unsafe struct View<A, B>
        where A : unmanaged
        where B : unmanaged
    {
        readonly Pool pool_a;
        readonly Pool pool_b;

        public View(Pool pool_a, Pool pool_b)
        {
            this.pool_a = pool_a;
            this.pool_b = pool_b;
        }

        public readonly ref A Get1(int entity) => ref Component.GetComponent(entity, pool_a.Set, pool_a.Count, (A*)pool_a.BytePtr);
        public readonly ref B Get2(int entity) => ref Component.GetComponent(entity, pool_b.Set, pool_b.Count, (B*)pool_b.BytePtr);

        public ReverseView_Enumerator2 GetEnumerator() => new ReverseView_Enumerator2(0, pool_a, pool_b);
    }

    public readonly unsafe struct View<A, B, C>
        where A : unmanaged
        where B : unmanaged
        where C : unmanaged
    {
        readonly Pool pool_a;
        readonly Pool pool_b;
        readonly Pool pool_c;

        public View(Pool pool_a, Pool pool_b, Pool pool_c)
        {
            this.pool_a = pool_a;
            this.pool_b = pool_b;
            this.pool_c = pool_c;
        }

        public readonly ref A Get1(int entity) => ref Component.GetComponent(entity, pool_a.Set, pool_a.Count, (A*)pool_a.BytePtr);
        public readonly ref B Get2(int entity) => ref Component.GetComponent(entity, pool_b.Set, pool_b.Count, (B*)pool_b.BytePtr);
        public readonly ref C Get3(int entity) => ref Component.GetComponent(entity, pool_c.Set, pool_c.Count, (C*)pool_c.BytePtr);

        public ReverseView_Enumerator3 GetEnumerator() => new ReverseView_Enumerator3(0, pool_a, pool_b, pool_c);
    }

    public readonly unsafe struct View<A, B, C, D>
        where A : unmanaged
        where B : unmanaged
        where C : unmanaged
        where D : unmanaged
    {
        readonly Pool pool_a;
        readonly Pool pool_b;
        readonly Pool pool_c;
        readonly Pool pool_d;

        public View(Pool pool_a, Pool pool_b, Pool pool_c, Pool pool_d)
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

        public ReverseView_Enumerator4 GetEnumerator() => new ReverseView_Enumerator4(0, pool_a, pool_b, pool_c, pool_d);
    }
}
