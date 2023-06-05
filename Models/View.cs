using NttSharp.Collections;
using NttSharp.Entities;
using NttSharp.Logic;
using System.Runtime.CompilerServices;

namespace NttSharp.Models
{
    public unsafe ref struct View_Enumerator1
    {
        public readonly ntt Current => value;

        int index;
        ntt value;
        ref readonly ntt* pool_a;

        public View_Enumerator1(int index, in Pool pool_a)
        {
            this.value = 0;
            this.index = pool_a.GetDenseLength() - 1;
            this.pool_a = ref pool_a.Set.Body;
        }

        public unsafe bool MoveNext()
        {
            ntt* ptr_a = pool_a;

            while (index >= 0)
            {
                value = Dense.PeekUnchecked(ptr_a, index--);

                return true;
            }
            return false;
        }
    }

    public ref struct View_Enumerator2
    {
        public readonly ntt Current => value;

        ntt value;
        int index;
        ref readonly SparseSet pool_a;
        ref readonly SparseSet pool_b;

        public View_Enumerator2(int index, in Pool pool_a, in Pool pool_b)
        {
            this.value = 0;
            this.index = pool_a.GetDenseLength() - 1;
            this.pool_a = ref pool_a.Set;
            this.pool_b = ref pool_b.Set;
        }

        public unsafe bool MoveNext()
        {
            ntt* ptr_a = pool_a.Body;
            ntt* ptr_b = pool_b.Body;
            int ptr_b_slen = pool_b._SparseLength;
            int ptr_b_dlen = pool_b._DenseLength;

            while (index >= 0)
            {
                value = Dense.PeekUnchecked(ptr_a, index--);

                if (Sparse.Contains(ptr_b, value, ptr_b_slen, ptr_b_dlen))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public ref struct View_Enumerator3
    {
        public readonly ntt Current => value;

        ntt value;
        int index;
        ref readonly SparseSet pool_a;
        ref readonly SparseSet pool_b;
        ref readonly SparseSet pool_c;

        public View_Enumerator3(int index, in Pool pool_a, in Pool pool_b, in Pool pool_c)
        {
            this.value = 0;
            this.index = pool_a.GetDenseLength() - 1;
            this.pool_a = ref pool_a.Set;
            this.pool_b = ref pool_b.Set;
            this.pool_c = ref pool_c.Set;
        }

        public unsafe bool MoveNext()
        {
            ntt* ptr_a = pool_a.Body;
            ntt* ptr_b = pool_b.Body;
            int ptr_b_slen = pool_b._SparseLength;
            int ptr_b_dlen = pool_b._DenseLength;
            ntt* ptr_c = pool_c.Body;
            int ptr_c_slen = pool_c._SparseLength;
            int ptr_c_dlen = pool_c._DenseLength;

            while (index >= 0)
            {
                value = Dense.PeekUnchecked(ptr_a, index--);

                if (Sparse.Contains(ptr_b, value, ptr_b_slen, ptr_b_dlen) &&
                    Sparse.Contains(ptr_c, value, ptr_c_slen, ptr_c_dlen))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public ref struct View_Enumerator4
    {
        public readonly ntt Current => value;

        ntt value;
        int index;
        ref readonly SparseSet pool_a;
        ref readonly SparseSet pool_b;
        ref readonly SparseSet pool_c;
        ref readonly SparseSet pool_d;

        public View_Enumerator4(int index, in Pool pool_a, in Pool pool_b, in Pool pool_c, in Pool pool_d)
        {
            this.value = 0;
            this.index = pool_a.GetDenseLength() - 1;
            this.pool_a = ref pool_a.Set;
            this.pool_b = ref pool_b.Set;
            this.pool_c = ref pool_c.Set;
            this.pool_d = ref pool_d.Set;
        }

        public unsafe bool MoveNext()
        {
            ntt* ptr_a = pool_a.Body;
            ntt* ptr_b = pool_b.Body;
            int ptr_b_slen = pool_b._SparseLength;
            int ptr_b_dlen = pool_b._DenseLength;
            ntt* ptr_c = pool_c.Body;
            int ptr_c_slen = pool_c._SparseLength;
            int ptr_c_dlen = pool_c._DenseLength;
            ntt* ptr_d = pool_d.Body;
            int ptr_d_slen = pool_d._SparseLength;
            int ptr_d_dlen = pool_d._DenseLength;

            while (index >= 0)
            {
                value = Dense.PeekUnchecked(ptr_a, index--);

                if (Sparse.Contains(ptr_b, value, ptr_b_slen, ptr_b_dlen) &&
                    Sparse.Contains(ptr_c, value, ptr_c_slen, ptr_c_dlen) &&
                    Sparse.Contains(ptr_d, value, ptr_d_slen, ptr_d_dlen))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public readonly unsafe ref struct View<A>
        where A : unmanaged
    {
        readonly ref readonly Pool pool_a;

        public View(in Pool pool_a)
        {
            this.pool_a = ref pool_a;
        }

        /// <summary>
        /// Gets a reference to
        /// the component of type: 
        /// <typeparamref name="A"/>,
        /// </summary>
        /// <param name="entity">the entity id</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref A Get1(ntt entity) => ref Component.GetComponent<A>(entity, in pool_a.Set, in pool_a.Bytes);
        
        public View_Enumerator1 GetEnumerator() => new View_Enumerator1(0, in pool_a);
    }

    public readonly ref struct View<A, B>
        where A : unmanaged
        where B : unmanaged
    {
        readonly ref readonly Pool pool_a;
        readonly ref readonly Pool pool_b;

        public View(in Pool pool_a, in Pool pool_b)
        {
            this.pool_a = ref pool_a;
            this.pool_b = ref pool_b;
        }

        /// <summary>
        /// Gets a reference to
        /// the component of type: 
        /// <typeparamref name="A"/>,
        /// </summary>
        /// <param name="entity">the entity id</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref A Get1(ntt entity) => ref Component.GetComponent<A>(entity, in pool_a.Set, in pool_a.Bytes);

        /// <summary>
        /// Gets a reference to
        /// the component of type: 
        /// <typeparamref name="B"/>,
        /// </summary>
        /// <param name="entity">the entity id</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref B Get2(ntt entity) => ref Component.GetComponent<B>(entity, in pool_b.Set, in pool_b.Bytes);

        public View_Enumerator2 GetEnumerator() => new View_Enumerator2(0, in pool_a, in pool_b);
    }

    public readonly ref struct View<A, B, C>
        where A : unmanaged
        where B : unmanaged
        where C : unmanaged
    {
        readonly ref readonly Pool pool_a;
        readonly ref readonly Pool pool_b;
        readonly ref readonly Pool pool_c;

        public View(in Pool pool_a, in Pool pool_b, in Pool pool_c)
        {
            this.pool_a = ref pool_a;
            this.pool_b = ref pool_b;
            this.pool_c = ref pool_c;
        }

        /// <summary>
        /// Gets a reference to
        /// the component of type: 
        /// <typeparamref name="A"/>,
        /// </summary>
        /// <param name="entity">the entity id</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref A Get1(ntt entity) => ref Component.GetComponent<A>(entity, in pool_a.Set, in pool_a.Bytes);

        /// <summary>
        /// Gets a reference to
        /// the component of type: 
        /// <typeparamref name="B"/>,
        /// </summary>
        /// <param name="entity">the entity id</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref B Get2(ntt entity) => ref Component.GetComponent<B>(entity, in pool_b.Set, in pool_b.Bytes);

        /// <summary>
        /// Gets a reference to
        /// the component of type: 
        /// <typeparamref name="C"/>,
        /// </summary>
        /// <param name="entity">the entity id</param> 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref C Get3(ntt entity) => ref Component.GetComponent<C>(entity, in pool_c.Set, in pool_c.Bytes);

        public View_Enumerator3 GetEnumerator() => new View_Enumerator3(0, in pool_a, in pool_b, in pool_c);
    }

    public readonly ref struct View<A, B, C, D>
        where A : unmanaged
        where B : unmanaged
        where C : unmanaged
        where D : unmanaged
    {
        readonly ref readonly Pool pool_a;
        readonly ref readonly Pool pool_b;
        readonly ref readonly Pool pool_c;
        readonly ref readonly Pool pool_d;

        public View(in Pool pool_a, in Pool pool_b, in Pool pool_c, in Pool pool_d)
        {
            this.pool_a = ref pool_a;
            this.pool_b = ref pool_b;
            this.pool_c = ref pool_c;
            this.pool_d = ref pool_d;
        }

        /// <summary>
        /// Gets a reference to
        /// the component of type: 
        /// <typeparamref name="A"/>,
        /// </summary>
        /// <param name="entity">the entity id</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref A Get1(ntt entity) => ref Component.GetComponent<A>(entity, in pool_a.Set, in pool_a.Bytes);

        /// <summary>
        /// Gets a reference to
        /// the component of type: 
        /// <typeparamref name="B"/>,
        /// </summary>
        /// <param name="entity">the entity id</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref B Get2(ntt entity) => ref Component.GetComponent<B>(entity, in pool_b.Set, in pool_b.Bytes);

        /// <summary>
        /// Gets a reference to
        /// the component of type: 
        /// <typeparamref name="C"/>,
        /// </summary>
        /// <param name="entity">the entity id</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref C Get3(ntt entity) => ref Component.GetComponent<C>(entity, in pool_c.Set, in pool_c.Bytes);

        /// <summary>
        /// Gets a reference to
        /// the component of type: 
        /// <typeparamref name="D"/>,
        /// </summary>
        /// <param name="entity">the entity id</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref D Get4(ntt entity) => ref Component.GetComponent<D>(entity, in pool_d.Set, in pool_d.Bytes);

        public View_Enumerator4 GetEnumerator() => new View_Enumerator4(0, in pool_a, in pool_b, in pool_c, in pool_d);
    }
}
