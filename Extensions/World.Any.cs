using NttSharp.Entities;

namespace NttSharp.Extensions
{
    public static class WorldAny
    {
        public static bool Any<A>(this World world) where A : unmanaged
        {
            Pool? pool = world.TryGetPool<A>();
            if (pool == null) return false;
            return pool.HasAny;
        }
    }
}
