using NttSharp.Models;

namespace NttSharp.Entities
{
    public sealed partial class World
    {
        /// <summary>
        /// Returns a view that
        /// will iterate all entities
        /// that contain a component of
        /// type <typeparamref name="A"/>.
        /// </summary>
        public View<A> View<A>() where A : unmanaged
        {
            ref Pool pool = ref GetPool<A>();

            return new View<A>(pool);
        }

        /// <summary>
        /// Returns a view that
        /// will iterate all entities
        /// that contain the following
        /// components: 
        /// <typeparamref name="A"/>,
        /// <typeparamref name="B"/>.
        /// </summary>
        public View<A, B> View<A, B>()
            where A : unmanaged
            where B : unmanaged
        {
            ref Pool pool_a = ref GetPool<A>();
            ref Pool pool_b = ref GetPool<B>();

            return new View<A, B>(pool_a, pool_b);
        }

        /// <summary>
        /// Returns a view that
        /// will iterate all entities
        /// that contain the following
        /// components: 
        /// <typeparamref name="A"/>,
        /// <typeparamref name="B"/>,
        /// <typeparamref name="C"/>.
        /// </summary>
        public View<A, B, C> View<A, B, C>()
            where A : unmanaged
            where B : unmanaged
            where C : unmanaged
        {
            ref Pool pool_a = ref GetPool<A>();
            ref Pool pool_b = ref GetPool<B>();
            ref Pool pool_c = ref GetPool<C>();

            return new View<A, B, C>(in pool_a, in pool_b, in pool_c);
        }

        /// <summary>
        /// Returns a view that
        /// will iterate all entities
        /// that contain the following
        /// components: 
        /// <typeparamref name="A"/>,
        /// <typeparamref name="B"/>,
        /// <typeparamref name="C"/>,
        /// <typeparamref name="D"/>.
        /// </summary>
        public View<A, B, C, D> View<A, B, C, D>()
            where A : unmanaged
            where B : unmanaged
            where C : unmanaged
            where D : unmanaged
        {
            ref Pool pool_a = ref GetPool<A>();
            ref Pool pool_b = ref GetPool<B>();
            ref Pool pool_c = ref GetPool<C>();
            ref Pool pool_d = ref GetPool<D>();

            return new View<A, B, C, D>(pool_a, pool_b, pool_c, pool_d);
        }
    }
}
