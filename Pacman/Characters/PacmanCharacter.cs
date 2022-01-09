using System;
using System.Collections.Generic;
using Pacman.Exceptions;
using Pacman.Input;
using Pacman.Output;

namespace Pacman
{
    public class PacmanCharacter : Character
    {
        private IUserInput _input;
        private IOutput _output;
        
        public PacmanCharacter(IUserInput input, IOutput output, Coordinate startingPosition)
            : base()
        {
            _input = input;
            _output = output;
            Coordinate = startingPosition;
            Symbol = DisplaySymbol.DefaultPacmanStartingSymbol;
            IsOnADot = false;
        }

        public override Coordinate GetMove(Grid grid)
        {
            string input = GetValidInput();

            if (input == Constants.Save)
            {
                throw new InputIsSaveException();
            }

            Coordinate coordinate = ConvertDirectionInputIntoCoordinate(input, grid.GetWidth(), grid.GetHeight());
            
            List<Coordinate> possibleMoves = GetPossibleMoves(Coordinate, grid);

            while (true)
            {
                foreach (var possibleMove in possibleMoves)
                {
                    if (possibleMove.GetRow() == coordinate.GetRow() &&
                        possibleMove.GetColumn() == coordinate.GetColumn())
                    {
                        Symbol = UpdateSymbol(input);
                        Coordinate = coordinate;
                        return coordinate;
                    }
                }

                _output.DisplayMessage(OutputMessages.InvalidInput);
                input = GetValidInput();
                
                coordinate = ConvertDirectionInputIntoCoordinate(input, grid.GetWidth(), grid.GetHeight());
            }
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

        private string GetValidInput()
        {
            string input = _input.GetUserInput();
            
            List<string> possibleInputs = new List<string>()
            {
                Constants.East,
                Constants.North,
                Constants.South,
                Constants.West,
                Constants.Save
            };

            while (!possibleInputs.Contains(input))
            {
                _output.DisplayMessage(OutputMessages.InvalidInput);
                input = _input.GetUserInput();
            }

            return input;
        }
    }
}