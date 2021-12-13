using System;

namespace Pacman.Output
{
    public class ConsoleOutput : IOutput
    {
        public void DisplayGrid(Grid grid)
        {
            for(int row = 0; row < 21; row++)
            {
                for(int column = 0; column < 19; column++)
                {
                    string point = grid.GetPoint(new Coordinate(row, column));
                    
                    if (point == DisplaySymbol.Wall)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else if(point == DisplaySymbol.Dot)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    Console.Write(point + " ");
                }
                Console.WriteLine();
            }
        }
    }
}