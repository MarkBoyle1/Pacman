using System;
using System.Linq;
using System.Threading;

namespace Pacman.Output
{
    public class ConsoleOutput : IOutput
    {
        private int _highScore;

        public void SetHighScore(int highScore)
        {
            _highScore = highScore;
        }
        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
        public void DisplayGrid(GameState gameState)
        {
            Grid grid = gameState.GetGrid();
            Console.Clear();
            Console.Write(" Lives: " + gameState.GetLivesLeft());
            Console.Write(" Score: " + gameState.GetScore());
            Console.Write(" Level: " + gameState.GetLevel());
            Console.WriteLine(" High Score: " + _highScore);

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
                    else if(point == DisplaySymbol.Monster)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
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

        public void DisplayDeathAnimation(GameState gameState)
        {
            Coordinate pacmanLocation = gameState.GetCharacterList().First().Coordinate;
            Grid grid = gameState.GetGrid();
            ConsoleColor monsterColour = ConsoleColor.Red;
            ConsoleColor wallColour = ConsoleColor.Blue;

            for (int i = 0; i < 10; i++)
            {
                Console.Clear();
                Console.Write(" Lives: " + gameState.GetLivesLeft());
                Console.Write(" Score: " + gameState.GetScore());
                Console.Write(" Level: " + gameState.GetLevel());
                Console.WriteLine(" High Score: " + _highScore);

                for (int row = 0; row < grid.GetHeight(); row++)
                {
                    for (int column = 0; column < grid.GetWidth(); column++)
                    {
                        string point = grid.GetPoint(new Coordinate(row, column));

                        if (row == pacmanLocation.GetRow() && column == pacmanLocation.GetColumn())
                        {
                            point = DisplaySymbol.Monster;
                            monsterColour = monsterColour == ConsoleColor.Red ? ConsoleColor.Cyan : ConsoleColor.Yellow;
                            Console.ForegroundColor = monsterColour;
                        }
                        else if (point == DisplaySymbol.Wall)
                        {
                            Console.ForegroundColor = wallColour;
                        }
                        else if (point == DisplaySymbol.Dot)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else if (point == DisplaySymbol.Monster)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }

                        Console.Write(point + " ");
                    }

                    Console.WriteLine();
                }
                wallColour = wallColour == ConsoleColor.Blue ? ConsoleColor.White : ConsoleColor.Blue;

                Thread.Sleep(200);
            }
        }
    }
}