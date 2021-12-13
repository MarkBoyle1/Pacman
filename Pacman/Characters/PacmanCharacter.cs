using Pacman.Exceptions;
using Pacman.Input;

namespace Pacman
{
    public class PacmanCharacter : Character
    {
        private IUserInput _input;
        public PacmanCharacter(IUserInput input)
            : base()
        {
            _input = input;
            Coordinate = new Coordinate(9, 11);
            Symbol = DisplaySymbol.DefaultPacmanStartingSymbol;
        }

        public override Coordinate GetMove()
        {
            string input = _input.GetUserInput();

            Symbol = UpdateSymbol(input);
            return ConvertDirectionInputIntoCoordinate(input);
        }

        private string UpdateSymbol(string input)
        {
            switch (input)
            {
                case Constants.East:
                    return DisplaySymbol.PacmanEastFacing;
                case Constants.West:
                    return DisplaySymbol.PacmanWestFacing;
                case Constants.North:
                    return DisplaySymbol.PacmanNorthFacing;
                case Constants.South:
                    return DisplaySymbol.PacmanSouthFacing;
                default:
                    throw new InvalidInputException();
            }
        }
    }
}