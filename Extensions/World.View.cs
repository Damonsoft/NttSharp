using NttSharp.Entities;
using NttSharp.Models;

namespace NttSharp.Extensions
{
    public static class WorldView
    {
        public static View<A> View<A>(this World world) where A : unmanaged
        {
            Pool pool = world.GetPool<A>();

            return new View<A>(pool);
        }

        public static View<A, B> View<A, B>(this World world)
            where A : unmanaged
            where B : unmanaged
        {
            Pool pool_a = world.GetPool<A>();
            Pool pool_b = world.GetPool<B>();

            return new View<A, B>(pool_a, pool_b);
        }

        public static View<A, B, C> View<A, B, C>(this World world)
            where A : unmanaged
            where B : unmanaged
            where C : unmanaged
        {
            Pool pool_a = world.GetPool<A>();
            Pool pool_b = world.GetPool<B>();
            Pool pool_c = world.GetPool<C>();

            return new View<A, B, C>(pool_a, pool_b, pool_c);
        }

        public static View<A, B, C, D> View<A, B, C, D>(this World world)
            where A : unmanaged
            where B : unmanaged
            where C : unmanaged
            where D : unmanaged
        {
            Pool pool_a = world.GetPool<A>();
            Pool pool_b = world.GetPool<B>();
            Pool pool_c = world.GetPool<C>();
            Pool pool_d = world.GetPool<D>();

            return new View<A, B, C, D>(pool_a, pool_b, pool_c, pool_d);
        }
    }
}
