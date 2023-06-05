using NttSharp.Entities;
using NttSharp.Logic;

namespace NttSharp.Models
{
    public unsafe ref struct ReverseView_Enumerator1
    {
        public readonly ntt Current => value;

        int index;
        ntt value;
        ref readonly ntt* pool_a;

        public ReverseView_Enumerator1(int index, in Pool pool_a)
        {
            this.index = 0;
            this.value = 0;
            this.pool_a = ref pool_a.Set.Body;
        }

        public unsafe bool MoveNext()
        {
            ntt* ptr_a = pool_a;

            while (index < Dense.PeekLength(ptr_a))
            {
                value = Dense.PeekUnchecked(ptr_a, index++);

                return true;
            }
            return false;
        }
    }

    public unsafe ref struct ReverseView_Enumerator2
    {
        public readonly ntt Current => value;

        int index;
        ntt value;
        ref readonly ntt* pool_a;
        ref readonly ntt* pool_b;
        ref readonly ntt* pool_c;

        public ReverseView_Enumerator2(int index, in Pool pool_a, in Pool pool_b)
        {
            this.index = index;
            this.value = 0;
            this.pool_a = ref pool_a.Set.Body;
            this.pool_b = ref pool_b.Set.Body;
        }

        public unsafe bool MoveNext()
        {
            ntt* ptr_a = pool_a;
            ntt* ptr_b = pool_b;

            while (index < Dense.PeekLength(ptr_a))
            {
                value = Dense.PeekUnchecked(ptr_a, index++);

                if (Sparse.Contains(ptr_b, value))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public unsafe ref struct ReverseView_Enumerator3
    {
        public readonly ntt Current => value;

        int index;
        ntt value;
        ref readonly ntt* pool_a;
        ref readonly ntt* pool_b;
        ref readonly ntt* pool_c;

        public ReverseView_Enumerator3(int index, in Pool pool_a, in Pool pool_b, in Pool pool_c)
        {
            this.index = index;
            this.value = 0;
            this.pool_a = ref pool_a.Set.Body;
            this.pool_b = ref pool_b.Set.Body;
            this.pool_c = ref pool_c.Set.Body;
        }

        public unsafe bool MoveNext()
        {
            ntt* ptr_a = pool_a;
            ntt* ptr_b = pool_b;
            ntt* ptr_c = pool_c;

            while (index < Dense.PeekLength(ptr_a))
            {
                value = Dense.PeekUnchecked(ptr_a, index++);

                if (Sparse.Contains(ptr_b, value) &&
                    Sparse.Contains(ptr_c, value))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public unsafe ref struct ReverseView_Enumerator4
    {
        public readonly ntt Current => value;

        int index;
        ntt value;
        ref readonly ntt* pool_a;
        ref readonly ntt* pool_b;
        ref readonly ntt* pool_c;
        ref readonly ntt* pool_d;

        public ReverseView_Enumerator4(int index, in Pool pool_a, in Pool pool_b, in Pool pool_c, in Pool pool_d)
        {
            this.index = index;
            this.value = 0;
            this.pool_a = ref pool_a.Set.Body;
            this.pool_b = ref pool_b.Set.Body;
            this.pool_c = ref pool_c.Set.Body;
            this.pool_d = ref pool_d.Set.Body;
        }

        public unsafe bool MoveNext()
        {
            ntt* ptr_a = pool_a;
            ntt* ptr_b = pool_b;
            ntt* ptr_c = pool_c;
            ntt* ptr_d = pool_d;

            while (index < Dense.PeekLength(ptr_a))
            {
                value = Dense.PeekUnchecked(ptr_a, index++);

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

    public readonly unsafe ref struct ReverseView<A> where A : unmanaged
    {
        readonly ref readonly Pool pool_a;

        public ReverseView(in Pool pool_T)
        {
            this.pool_a = ref pool_T;
        }

        public readonly ref A Get1(ntt entity) => ref Component.GetComponent<A>(entity, in pool_a.Set, in pool_a.Bytes);

        public ReverseView_Enumerator1 GetEnumerator() => new ReverseView_Enumerator1(0, in pool_a);
    }

    public readonly unsafe ref struct ReverseView<A, B>
        where A : unmanaged
        where B : unmanaged
    {
        readonly ref readonly Pool pool_a;
        readonly ref readonly Pool pool_b;

        public ReverseView(
            in Pool pool_a,
            in Pool pool_b)
        {
            this.pool_a = ref pool_a;
            this.pool_b = ref pool_b;
        }

        public readonly ref A Get1(ntt entity) => ref Component.GetComponent<A>(entity, in pool_a.Set, in pool_a.Bytes);
        public readonly ref B Get2(ntt entity) => ref Component.GetComponent<B>(entity, in pool_b.Set, in pool_b.Bytes);

        public ReverseView_Enumerator2 GetEnumerator() => new ReverseView_Enumerator2(0, in pool_a, in pool_b);
    }

    public readonly unsafe ref struct ReverseView<A, B, C>
        where A : unmanaged
        where B : unmanaged
        where C : unmanaged
    {
        readonly ref readonly Pool pool_a;
        readonly ref readonly Pool pool_b;
        readonly ref readonly Pool pool_c;

        public ReverseView(
            in Pool pool_a,
            in Pool pool_b,
            in Pool pool_c)
        {
            this.pool_a = ref pool_a;
            this.pool_b = ref pool_b;
            this.pool_c = ref pool_c;
        }

        public readonly ref A Get1(ntt entity) => ref Component.GetComponent<A>(entity, in pool_a.Set, in pool_a.Bytes);
        public readonly ref B Get2(ntt entity) => ref Component.GetComponent<B>(entity, in pool_b.Set, in pool_b.Bytes);
        public readonly ref C Get3(ntt entity) => ref Component.GetComponent<C>(entity, in pool_c.Set, in pool_c.Bytes);
        public ReverseView_Enumerator3 GetEnumerator() => new ReverseView_Enumerator3(0, in pool_a, in pool_b, in pool_c);
    }

    public readonly unsafe ref struct ReverseView<A, B, C, D>
        where A : unmanaged
        where B : unmanaged
        where C : unmanaged
        where D : unmanaged
    {
        readonly ref readonly Pool pool_a;
        readonly ref readonly Pool pool_b;
        readonly ref readonly Pool pool_c;
        readonly ref readonly Pool pool_d;

        public ReverseView(
            in Pool pool_a,
            in Pool pool_b,
            in Pool pool_c,
            in Pool pool_d)
        {
            this.pool_a = ref pool_a;
            this.pool_b = ref pool_b;
            this.pool_c = ref pool_c;
            this.pool_d = ref pool_d;
        }

        public readonly ref A Get1(ntt entity) => ref Component.GetComponent<A>(entity, in pool_a.Set, in pool_a.Bytes);
        public readonly ref B Get2(ntt entity) => ref Component.GetComponent<B>(entity, in pool_b.Set, in pool_b.Bytes);
        public readonly ref C Get3(ntt entity) => ref Component.GetComponent<C>(entity, in pool_c.Set, in pool_c.Bytes);
        public readonly ref D Get4(ntt entity) => ref Component.GetComponent<D>(entity, in pool_d.Set, in pool_d.Bytes);

        public ReverseView_Enumerator4 GetEnumerator() => new ReverseView_Enumerator4(0, in pool_a, in pool_b, in pool_c, in pool_d);
    }
}
