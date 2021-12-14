using Pacman.Input;
using Pacman.Output;

namespace Pacman
{
    public class Engine
    {
        private GridBuilder _gridBuilder = new GridBuilder();
        private IOutput _output = new ConsoleOutput();
        private IUserInput _input = new UserInput();

        public void RunProgram()
        {
            ILevel level = new OriginalLayoutLevel();
            Grid grid = _gridBuilder.GenerateInitialGrid(level.GetGridWidth(), level.GetGridHeight(),
                level.GetWallCoordinates(), level.GetBlankSpacesCoordinates());
            grid = PlacePacmanOnStartingPosition(grid, level.GetPacmanStartingPosition());
            _output.DisplayGrid(grid);

            Character character = new PacmanCharacter(_input, _output, level.GetPacmanStartingPosition());

            while(true)
            {
                grid = MakeCharacterMove(grid, character);
                _output.DisplayGrid(grid);
            }
        }
        public Grid PlacePacmanOnStartingPosition(Grid grid, Coordinate startingPosition)
        {
            return _gridBuilder.UpdateGrid(grid, DisplaySymbol.DefaultPacmanStartingSymbol, startingPosition);
        }

        public Grid MakeCharacterMove(Grid grid, Character character)
        {
            grid = _gridBuilder.UpdateGrid(grid, DisplaySymbol.BlankSpace, character.GetCoordinate());
            Coordinate coordinate = character.GetMove(grid);

            if (grid.GetPoint(coordinate) == DisplaySymbol.Dot)
            {
                string eatingSymbol =
                    character.Symbol is DisplaySymbol.PacmanEastFacing or DisplaySymbol.PacmanWestFacing
                        ? DisplaySymbol.PacmanHorizontalEating
                        : DisplaySymbol.PacmanVerticalEating;
                
                grid = _gridBuilder.UpdateGrid(grid, character.Symbol, coordinate);
                _output.DisplayGrid(grid);
                
                grid = _gridBuilder.UpdateGrid(grid, eatingSymbol, coordinate);
                _output.DisplayGrid(grid);
            }
            
            return _gridBuilder.UpdateGrid(grid, character.Symbol, coordinate);
        }
    }
}