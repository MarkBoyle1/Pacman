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
            grid = PlacePacmanOnStartingPosition(grid, level);
            _output.DisplayGrid(grid);

            Character character = new PacmanCharacter(_input);

            for (int i = 0; i < 5; i++)
            {
                grid = MakeCharacterMove(grid, character);
                _output.DisplayGrid(grid);
            }
        }
        public Grid PlacePacmanOnStartingPosition(Grid grid, ILevel level)
        {
            return _gridBuilder.UpdateGrid(grid, DisplaySymbol.DefaultPacmanStartingSymbol, level.GetPacmanStartingPosition());
        }

        public Grid MakeCharacterMove(Grid grid, Character character)
        {
            grid = _gridBuilder.UpdateGrid(grid, DisplaySymbol.BlankSpace, character.GetCoordinate());
            Coordinate coordinate = character.GetMove();
            return _gridBuilder.UpdateGrid(grid, character.Symbol, coordinate);
        }
    }
}