using NttSharp.Models;

namespace NttSharp.Entities
{
    public static class WorldReverseView
    {
        public static ReverseView<A> ReverseView<A>(this World world)
            where A : unmanaged
        {
            Pool pool_a = world.GetPool<A>();

            return new ReverseView<A>(pool_a);
        }

        public static ReverseView<A, B> ReverseView<A, B>(this World world)
            where A : unmanaged
            where B : unmanaged
        {
            Pool pool_a = world.GetPool<A>();
            Pool pool_b = world.GetPool<B>();

            return new ReverseView<A, B>(pool_a, pool_b);
        }

        public static ReverseView<A, B, C> ReverseView<A, B, C>(this World world)
            where A : unmanaged
            where B : unmanaged
            where C : unmanaged
        {
            Pool pool_a = world.GetPool<A>();
            Pool pool_b = world.GetPool<B>();
            Pool pool_c = world.GetPool<C>();

            return new ReverseView<A, B, C>(pool_a, pool_b, pool_c);
        }

        public static ReverseView<A, B, C, D> ReverseView<A, B, C, D>(this World world)
            where A : unmanaged
            where B : unmanaged
            where C : unmanaged
            where D : unmanaged
        {
            Pool pool_a = world.GetPool<A>();
            Pool pool_b = world.GetPool<B>();
            Pool pool_c = world.GetPool<C>();
            Pool pool_d = world.GetPool<D>();

            return new ReverseView<A, B, C, D>(pool_a, pool_b, pool_c, pool_d);
        }
    }
}
