using System.Collections.Generic;

namespace Pacman
{
    public class Grid
    {
        public string[][] Surface;

        public Grid(string[][] surface)
        {
            Surface = surface;
        }

        public string GetPoint(Coordinate coordinate)
        {
            return Surface[coordinate.GetRow()][coordinate.GetColumn()];
        }

        public int GetWidth()
        {
            return Surface[0].Length;
        }

        public int GetHeight()
        {
            return Surface.Length;
        }

        public List<Coordinate> GetPossibleMoves(Coordinate coordinate)
        {
            List<Coordinate> possibleMoves = new List<Coordinate>();

            List<Coordinate> surroundingSpaces = new List<Coordinate>()
            {
                new Coordinate(coordinate.GetRow() + 1, coordinate.GetColumn()),
                new Coordinate(coordinate.GetRow() - 1, coordinate.GetColumn()),
                new Coordinate(coordinate.GetRow(), coordinate.GetColumn() + 1),
                new Coordinate(coordinate.GetRow(), coordinate.GetColumn() - 1),
            };

            foreach (var space in surroundingSpaces)
            {
                if (GetPoint(space) != DisplaySymbol.Wall)
                {
                    possibleMoves.Add(space);
                }
            }

            return possibleMoves;
        }
    }
}