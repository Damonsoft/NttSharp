using NttSharp.Models;

namespace NttSharp.Entities
{
    public sealed partial class World
    {
        public ReverseView<A> ReverseView<A>()
            where A : unmanaged
        {
            Pool pool_a = GetPool<A>();

            return new ReverseView<A>(pool_a);
        }

        public ReverseView<A, B> ReverseView<A, B>()
            where A : unmanaged
            where B : unmanaged
        {
            Pool pool_a = GetPool<A>();
            Pool pool_b = GetPool<B>();

            return new ReverseView<A, B>(pool_a, pool_b);
        }

        public ReverseView<A, B, C> ReverseView<A, B, C>()
            where A : unmanaged
            where B : unmanaged
            where C : unmanaged
        {
            Pool pool_a = GetPool<A>();
            Pool pool_b = GetPool<B>();
            Pool pool_c = GetPool<C>();

            return new ReverseView<A, B, C>(pool_a, pool_b, pool_c);
        }

        public ReverseView<A, B, C, D> ReverseView<A, B, C, D>()
            where A : unmanaged
            where B : unmanaged
            where C : unmanaged
            where D : unmanaged
        {
            Pool pool_a = GetPool<A>();
            Pool pool_b = GetPool<B>();
            Pool pool_c = GetPool<C>();
            Pool pool_d = GetPool<D>();

            return new ReverseView<A, B, C, D>(pool_a, pool_b, pool_c, pool_d);
        }
    }
}
