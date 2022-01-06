using System;
using System.Collections.Generic;
using Pacman.Input;
using Pacman.Output;

namespace Pacman
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine engine = new Engine(new OriginalLayout(), new UserInput(), new ConsoleOutput());
            
            engine.RunProgram();
        }
    }
}