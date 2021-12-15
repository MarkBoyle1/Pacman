using System.Collections.Generic;

namespace Pacman
{
    public class Grid
    {
        public string[][] Surface;
        private int _dotsRemaining;

        public Grid(string[][] surface, int dotsRemaining)
        {
            Surface = surface;
            _dotsRemaining = dotsRemaining;
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

        public int GetDotsRemaining()
        {
            return _dotsRemaining;
        }

        public List<Coordinate> GetPossibleMoves(Coordinate coordinate)
        {
            List<Coordinate> possibleMoves = new List<Coordinate>();

            List<Coordinate> surroundingSpaces = new List<Coordinate>()
            {
                WrapAroundGridIfRequired(new Coordinate(coordinate.GetRow() + 1, coordinate.GetColumn())),
                WrapAroundGridIfRequired(new Coordinate(coordinate.GetRow() - 1, coordinate.GetColumn())),
                WrapAroundGridIfRequired(new Coordinate(coordinate.GetRow(), coordinate.GetColumn() + 1)), 
                WrapAroundGridIfRequired(new Coordinate(coordinate.GetRow(), coordinate.GetColumn() - 1)),
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
        
        public Coordinate WrapAroundGridIfRequired(Coordinate coordinate)
        {
            int xCoordinate = AdjustRowCoordinate(coordinate.GetRow());
            int yCoordinate = AdjustColumnCoordinate(coordinate.GetColumn());
        
            return new Coordinate(xCoordinate, yCoordinate);
        }
        
        private int AdjustRowCoordinate(int coordinate)
        {
            if (coordinate < 0)
            {
                return GetHeight() - 1;
            }
            
            if (coordinate > GetHeight()- 1)
            {
                return 0;
            }
            
            return coordinate;
        }
        
        private int AdjustColumnCoordinate(int coordinate)
        {
            if (coordinate < 0)
            {
                return GetWidth() - 1;
            }
            
            if (coordinate > GetWidth()- 1)
            {
                return 0;
            }
            
            return coordinate;
        }
        
        
    }
}