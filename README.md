# Ntt# - An Entity Component System for C#
`Ntt#` is a small library for creating games easily and efficiently without sparing any performance..<br/>

## Code Example

```cs
using NttSharp.Entities;
using NttSharp.Models;

namespace Test.Console
{
    public partial class Program
    {
        public struct Position
        {
            public int X;
            public int Y;

            public Position(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        public struct Velocity
        {
            public int X;
            public int Y;

            public Velocity(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        static void Main(string[] args)
        {
            World world = new World();

            foreach (int offset in Enumerable.Range(0, 10))
            {
                int entity = world.Create();

                world.Assign(entity, new Position(offset, offset));

                if (offset % 2 is 0)
                {
                    world.Assign(entity, new Velocity(offset, offset));
                }
            }

            world.Each((ref Position position, ref Velocity velocity) =>
            {
                //
            });

            world.Each((int entity, ref Position position, ref Velocity velocity) =>
            {
                //
            });

            View<Position, Velocity> view = world.View<Position, Velocity>();

            foreach (var entity in view)
            {
                ref Position position = ref view.Get1(entity);
                ref Velocity velocity = ref view.Get2(entity);

                //
            }
        }
    }
}
```
