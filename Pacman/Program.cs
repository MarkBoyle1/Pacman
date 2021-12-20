using System;
using System.Collections.Generic;

namespace Pacman
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine engine = new Engine(new OriginalLayout());
            
            engine.RunProgram();
        }
    }
}