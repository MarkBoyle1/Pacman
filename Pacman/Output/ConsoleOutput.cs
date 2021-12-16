using System;
using System.Threading;

namespace Pacman.Output
{
    public class ConsoleOutput : IOutput
    {
        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
        public void DisplayGrid(GameState gameState)
        {
            Grid grid = gameState.GetGrid();
            Console.Clear();
            Console.Write("Dots Remaining: " + gameState.GetGrid().GetDotsRemaining());
            Console.WriteLine("Level: " + gameState.GetLevel());

            for(int row = 0; row < grid.GetHeight(); row++)
            {
                for(int column = 0; column < grid.GetWidth(); column++)
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
            Thread.Sleep(200);
        }
    }
}