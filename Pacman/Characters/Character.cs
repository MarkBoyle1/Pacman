using Pacman.Exceptions;

namespace Pacman
{
    public abstract class Character
    {
        public string Symbol;
        public Coordinate Coordinate;
        public virtual Coordinate GetMove()
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

        public Coordinate ConvertDirectionInputIntoCoordinate(string input)
        {
            switch (input)
            {
                case Constants.East:
                    return new Coordinate(Coordinate.GetRow(), Coordinate.GetColumn() + 1);
                case Constants.West:
                    return new Coordinate(Coordinate.GetRow(), Coordinate.GetColumn() - 1);
                case Constants.North:
                    return new Coordinate(Coordinate.GetRow() - 1, Coordinate.GetColumn());
                case Constants.South:
                    return new Coordinate(Coordinate.GetRow() + 1, Coordinate.GetColumn());
                default:
                    throw new InvalidInputException();
            }
        }
    }
}