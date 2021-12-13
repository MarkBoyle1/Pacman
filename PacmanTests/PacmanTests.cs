using Pacman;
using Pacman.Input;
using Xunit;

namespace PacmanTests
{
    public class PacmanTests
    {
        private GridBuilder _gridBuilder = new GridBuilder();
        private Engine _engine = new Engine();
        
        [Fact]
        public void given_StartingLocationEqualsNineEleven_when_PlacePacmanOnStartingLocation_then_NineElevenEqualsPacman()
        {
            ILevel level = new OriginalLayoutLevel();
            
            Grid grid = _gridBuilder.GenerateEmptyGrid(level.GetGridWidth(), level.GetGridHeight());

            grid = _engine.PlacePacmanOnStartingPosition(grid, level);
            
            Assert.Equal(DisplaySymbol.DefaultPacmanStartingSymbol, grid.GetPoint(level.GetPacmanStartingPosition()));
        }
        
        [Fact]
        public void given_InputEqualsEast_and_currentLocationEqualsNineEleven_then_TenElevenEqualsPacmanEastFacing()
        {
            IUserInput input = new TestInput(Constants.East);
            Character pacman = new PacmanCharacter(input);
            Coordinate move = pacman.GetMove();
            
            Assert.Equal(9, move.GetRow());            
            Assert.Equal(12, move.GetColumn());
        }

        [Fact]
        public void given_InputEqualsEast_when_GetMove_then_SymbolEqualsPacmanEastFacing()
        {
            IUserInput input = new TestInput(Constants.East);
            Character pacman = new PacmanCharacter(input);
            Coordinate move = pacman.GetMove();
            
            Assert.Equal(DisplaySymbol.PacmanEastFacing, pacman.GetSymbol());
        }
        
        [Fact]
        public void given_InputEqualsNorth_when_GetMove_then_SymbolEqualsPacmanNorthFacing()
        {
            IUserInput input = new TestInput(Constants.North);
            Character pacman = new PacmanCharacter(input);
            Coordinate move = pacman.GetMove();
            
            Assert.Equal(DisplaySymbol.PacmanNorthFacing, pacman.GetSymbol());
        }
        
        [Fact]
        public void given_InputEqualsNorth_when_MakeCharacterMove_then_gridIsUpdated()
        {
            Grid grid = _gridBuilder.GenerateEmptyGrid(19, 21);
            grid = _engine.PlacePacmanOnStartingPosition(grid, new OriginalLayoutLevel());

            IUserInput input = new TestInput(Constants.North);
            Character pacman = new PacmanCharacter(input);
            grid = _engine.MakeCharacterMove(grid, pacman);
            
            Assert.Equal(DisplaySymbol.PacmanNorthFacing, grid.GetPoint(new Coordinate(8,11)));
        }
        
        [Fact]
        public void given_InputEqualsWest_when_MakeCharacterMove_then_gridIsUpdated()
        {
            Grid grid = _gridBuilder.GenerateEmptyGrid(19, 21);
            grid = _engine.PlacePacmanOnStartingPosition(grid, new OriginalLayoutLevel());

            IUserInput input = new TestInput(Constants.West);
            Character pacman = new PacmanCharacter(input);
            grid = _engine.MakeCharacterMove(grid, pacman);
            
            Assert.Equal(DisplaySymbol.PacmanWestFacing, grid.GetPoint(new Coordinate(9,10)));
        }
    }
}