using Pacman.Exceptions;

namespace Pacman
{
    public abstract class Character
    {
        public string Symbol;
        public Coordinate Coordinate;
        public virtual Coordinate GetMove(Grid grid)
        {
            return new Coordinate(1, 1);
        }

        public Coordinate GetCoordinate()
        {
            return Coordinate;
        }

        public string GetSymbol()
        {
            return Symbol;
        }

        public Coordinate ConvertDirectionInputIntoCoordinate(string input, int gridWidth, int gridHeight)
        {
            Coordinate coordinate;
            
            switch (input)
            {
                case Constants.East:
                    coordinate = new Coordinate(Coordinate.GetRow(), Coordinate.GetColumn() + 1);
                    return WrapAroundGridIfRequired(coordinate, gridWidth, gridHeight);
                case Constants.West:
                    coordinate = new Coordinate(Coordinate.GetRow(), Coordinate.GetColumn() - 1);
                    return WrapAroundGridIfRequired(coordinate, gridWidth, gridHeight);
                case Constants.North:
                    coordinate = new Coordinate(Coordinate.GetRow() - 1, Coordinate.GetColumn());
                    return WrapAroundGridIfRequired(coordinate, gridWidth, gridHeight);
                case Constants.South:
                    coordinate = new Coordinate(Coordinate.GetRow() + 1, Coordinate.GetColumn());
                    return WrapAroundGridIfRequired(coordinate, gridWidth, gridHeight);
                default:
                    throw new InvalidInputException();
            }
        }
        
        private Coordinate WrapAroundGridIfRequired(Coordinate coordinate, int gridWidth, int gridHeight)
        {
            int xCoordinate = AdjustRowCoordinate(coordinate.GetRow(), gridHeight);
            int yCoordinate = AdjustColumnCoordinate(coordinate.GetColumn(), gridWidth);
        
            return new Coordinate(xCoordinate, yCoordinate);
        }
        
        private int AdjustRowCoordinate(int coordinate, int gridHeight)
        {
            if (coordinate < 0)
            {
                return gridHeight - 1;
            }
            
            if (coordinate > gridHeight- 1)
            {
                return 0;
            }
            
            return coordinate;
        }
        
        private int AdjustColumnCoordinate(int coordinate, int gridWidth)
        {
            if (coordinate < 0)
            {
                return gridWidth - 1;
            }
            
            if (coordinate > gridWidth- 1)
            {
                return 0;
            }
            
            return coordinate;
        }   
    }
}