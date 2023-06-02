using NttSharp.Models;

namespace NttSharp.Entities
{
    public sealed partial class World
    {
        public ReverseView<A> ReverseView<A>()
            where A : unmanaged
        {
            ref Pool pool_a = ref GetPool<A>();

            return new ReverseView<A>(in pool_a);
        }

        public ReverseView<A, B> ReverseView<A, B>()
            where A : unmanaged
            where B : unmanaged
        {
            ref Pool pool_a = ref GetPool<A>();
            ref Pool pool_b = ref GetPool<B>();

            return new ReverseView<A, B>(in pool_a, in pool_b);
        }

        public ReverseView<A, B, C> ReverseView<A, B, C>()
            where A : unmanaged
            where B : unmanaged
            where C : unmanaged
        {
            ref Pool pool_a = ref GetPool<A>();
            ref Pool pool_b = ref GetPool<B>();
            ref Pool pool_c = ref GetPool<C>();

            return new ReverseView<A, B, C>(pool_a, pool_b, pool_c);
        }

        public ReverseView<A, B, C, D> ReverseView<A, B, C, D>()
            where A : unmanaged
            where B : unmanaged
            where C : unmanaged
            where D : unmanaged
        {
            ref Pool pool_a = ref GetPool<A>();
            ref Pool pool_b = ref GetPool<B>();
            ref Pool pool_c = ref GetPool<C>();
            ref Pool pool_d = ref GetPool<D>();

            return new ReverseView<A, B, C, D>(pool_a, pool_b, pool_c, pool_d);
        }
    }
}
