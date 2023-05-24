using NttSharp.Extensions;
using NttSharp.Models;

namespace NttSharp.Entities
{
    public static class WorldFirst
    {
        public static bool TryFirst<A>(this World world, out Single<A> result) where A : unmanaged
		{
			Pool pool_a = world.GetPool<A>();

			foreach(int entity in world.AllEntites())
			{
				if (pool_a.Contains(entity))
				{
					result = new Single<A>(entity, pool_a);

					return true;
				}
			}
			result = default;

			return false;
		}

        public static bool TryFirst<A, B>(this World world, out Single<A, B> result)
			where A : unmanaged
			where B : unmanaged
		{
			Pool pool_a = world.GetPool<A>();
			Pool pool_b = world.GetPool<B>();

			foreach (int entity in world.AllEntites())
			{
				if (pool_a.Contains(entity) &&
					pool_b.Contains(entity))
				{
					result = new Single<A, B>(entity, pool_a, pool_b);

					return true;
				}
			}
			result = default;

			return false;
		}

        public static bool TryFirst<A, B, C>(this World world, out Single<A, B, C> result)
			where A : unmanaged
			where B : unmanaged
            where C : unmanaged
        {
            Pool pool_a = world.GetPool<A>();
            Pool pool_b = world.GetPool<B>();
			Pool pool_c = world.GetPool<C>();

            foreach (int entity in world.AllEntites())
            {
                if (pool_a.Contains(entity) &&
                    pool_b.Contains(entity))
                {
                    result = new Single<A, B, C>(entity, pool_a, pool_b, pool_c);

                    return true;
                }
            }
            result = default;

            return false;
        }

        public static Single<A> First<A>(this World world) where A : unmanaged
		{
			Pool pool_a = world.GetPool<A>();

			foreach (int entity in world.AllEntites())
			{
				if (pool_a.Contains(entity))
				{
					return new Single<A>(entity, pool_a);
				}	
			}
			throw new Exception();
		}

		public static Single<A, B> First<A, B>(this World world)
			where A : unmanaged
			where B : unmanaged
		{
			Pool pool_a = world.GetPool<A>();
			Pool pool_b = world.GetPool<B>();

			foreach (int entity in world.AllEntites())
			{
				if (pool_a.Contains(entity) &&
					pool_b.Contains(entity))
				{
					return new Single<A, B>(entity, pool_a, pool_b);
				}
			}
			throw new Exception();
		}

		public static Single<A, B, C> First<A, B, C>(this World world)
			where A : unmanaged
			where B : unmanaged
			where C : unmanaged
		{
			Pool pool_a = world.GetPool<A>();
			Pool pool_b = world.GetPool<B>();
			Pool pool_c = world.GetPool<C>();

			foreach (int entity in world.AllEntites())
			{
				if (pool_a.Contains(entity) &&
					pool_b.Contains(entity) &&
					pool_c.Contains(entity))
				{
					return new Single<A, B, C>(entity, pool_a, pool_b, pool_c);
				}
			}
			throw new Exception();
		}
	}
}
