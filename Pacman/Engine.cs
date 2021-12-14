using Pacman.Input;
using Pacman.Output;

namespace Pacman
{
    public class Engine
    {
        private GridBuilder _gridBuilder = new GridBuilder();
        private IOutput _output = new ConsoleOutput();
        private IUserInput _input = new UserInput();
        private int _gameScore = 0;

        public void RunProgram()
        {
            ILevel level = new OriginalLayoutLevel();
            Character pacman = new PacmanCharacter(_input, _output, level.GetPacmanStartingPosition());
            
            Grid grid = _gridBuilder.GenerateInitialGrid(level.GetGridWidth(), level.GetGridHeight(),
                level.GetWallCoordinates(), level.GetBlankSpacesCoordinates());
            
            grid = PlacePacmanOnStartingPosition(grid, pacman.Coordinate);
            _output.DisplayGrid(grid);
            
            while(true)
            {
                grid = MakeCharacterMove(grid, pacman);
                _output.DisplayGrid(grid);
            }
        }
        public Grid PlacePacmanOnStartingPosition(Grid grid, Coordinate startingPosition)
        {
            return _gridBuilder.UpdateGrid(grid, DisplaySymbol.DefaultPacmanStartingSymbol, startingPosition);
        }

        public GameState PlayOneTick()
        {
            return new GameState(_gameScore);
        }

        public Grid MakeCharacterMove(Grid grid, Character character)
        {
            grid = _gridBuilder.UpdateGrid(grid, DisplaySymbol.BlankSpace, character.GetCoordinate());
            Coordinate coordinate = character.GetMove(grid);

            if (grid.GetPoint(coordinate) == DisplaySymbol.Dot)
            {
                _gameScore++;
                MakeEatingAnimation(grid, character, coordinate);
            }
            
            return _gridBuilder.UpdateGrid(grid, character.Symbol, coordinate);
        }

        private void MakeEatingAnimation(Grid grid, Character character, Coordinate coordinate)
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
    }
}