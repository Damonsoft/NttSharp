# Ntt# - An Entity Component System for C#

`Ntt#` is a small library for creating games easily and efficiently without sparing performance.<br/>

## Introdutction

Traditionally in game development the inheritance route is what most developers go for when desiging their master-pieces.
This approach, while solid, comes with many caveats that can heavily impact the performance and stability of games and programs alike.
With this in mind developers have been experimenting with new ways to build their game logic that can meet the high performance and stability requirments of today's games.
One of those many approaches is the entity-component-system (shorthand _ECS_), which is a pattern that favors a component based approach over an inheritance based one.
This system seperates individual pieces of 'objects' into components and encourages a more dynamic yet performant way of structuring game data.</br>

## Code Example

```cs
using NttSharp.Entities;
using NttSharp.Models;

namespace NttSharp.Example.Console
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

    public partial class Program
    {
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
