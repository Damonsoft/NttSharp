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
            Pool pool = GetPool<A>();

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
            Pool pool_a = GetPool<A>();
            Pool pool_b = GetPool<B>();

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
            Pool pool_a = GetPool<A>();
            Pool pool_b = GetPool<B>();
            Pool pool_c = GetPool<C>();

            return new View<A, B, C>(pool_a, pool_b, pool_c);
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
            Pool pool_a = GetPool<A>();
            Pool pool_b = GetPool<B>();
            Pool pool_c = GetPool<C>();
            Pool pool_d = GetPool<D>();

            return new View<A, B, C, D>(pool_a, pool_b, pool_c, pool_d);
        }
    }
}
