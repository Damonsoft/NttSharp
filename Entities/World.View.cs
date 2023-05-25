using NttSharp.Models;

namespace NttSharp.Entities
{
    public sealed partial class World
    {
        public View<A> View<A>() where A : unmanaged
        {
            Pool pool = GetPool<A>();

            return new View<A>(pool);
        }

        public View<A, B> View<A, B>()
            where A : unmanaged
            where B : unmanaged
        {
            Pool pool_a = GetPool<A>();
            Pool pool_b = GetPool<B>();

            return new View<A, B>(pool_a, pool_b);
        }

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
