using System;
using System.Collections.Generic;

namespace Pacman
{
    class Program
    {
        static void Main(string[] args)
        {
            ILayout layout = new TestLayout
                (
                    new Coordinate(0, 0),

                    new List<Coordinate>()
                     {
                         new Coordinate(1, 1)
                         // new Coordinate(3, 4),
                         // new Coordinate(2, 3),
                         // new Coordinate(2, 4)
                     },
                    new List<Coordinate>(),
                    new List<Coordinate>(),
                    3,
                    3,
                    1
                );
            Engine engine = new Engine(new OriginalLayout());
            
            engine.RunProgram();
        }
    }
}