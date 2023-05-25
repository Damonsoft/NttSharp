namespace NttSharp.Entities
{
    public sealed partial class World
    {
        public bool Any<A>() where A : unmanaged
        {
            Pool? pool = TryGetPool<A>();
            if (pool == null) return false;
            return pool.HasAny;
        }
    }
}
