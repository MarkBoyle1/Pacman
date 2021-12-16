using System.Collections.Generic;
using Pacman;
using Pacman.Input;
using Pacman.Output;
using Xunit;

namespace PacmanTests
{
    public class PacmanTests
    {
        private GridBuilder _gridBuilder;
        private Engine _engine;
        private Grid _defaultGrid;

        public PacmanTests()
        {
            _gridBuilder = new GridBuilder();
            _engine = new Engine();
            _defaultGrid = _gridBuilder.GenerateEmptyGrid(19, 21);
        }
        
        [Fact]
        public void given_StartingLocationEqualsNineEleven_when_PlacePacmanOnStartingLocation_then_NineElevenEqualsPacman()
        {
            ILevel level = new OriginalLayoutLevel();
            
            Grid grid = _gridBuilder.GenerateEmptyGrid(level.GetGridWidth(), level.GetGridHeight());

            grid = _engine.PlacePacmanOnStartingPosition(grid, level.GetPacmanStartingPosition());
            
            Assert.Equal(DisplaySymbol.DefaultPacmanStartingSymbol, grid.GetPoint(level.GetPacmanStartingPosition()));
        }
        
        [Fact]
        public void given_InputEqualsEast_and_currentLocationEqualsElevenNine_when_GetMove_then_returns_CoordinateOfElevenTen()
        {
            IUserInput input = new TestInput(new List<string>{Constants.East});
            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), new Coordinate(11,9));
            Coordinate move = pacman.GetMove(_defaultGrid);
            
            Assert.Equal(11, move.GetRow());            
            Assert.Equal(10, move.GetColumn());
        }
        
        [Fact]
        public void given_InputEqualsWest_and_CurrentLocationEqualsZeroZero_and_GridWidthEqualsThree_when_GetMove_then_return_ZeroTwo()
        {
            IUserInput input = new TestInput(new List<string>{Constants.West});
            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), new Coordinate(0,0));
            Grid grid = _gridBuilder.GenerateEmptyGrid(3, 3);
            Coordinate move = pacman.GetMove(grid);
            
            Assert.Equal(0, move.GetRow());            
            Assert.Equal(2, move.GetColumn());
        }
        
        [Fact]
        public void given_InputEqualsNorth_and_currentLocationZeroZero_and_gridHeightEqualsThree_when_GetMove_then_return_TwoZero()
        {
            IUserInput input = new TestInput(new List<string>{Constants.North});
            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), new Coordinate(0,0));
            Grid grid = _gridBuilder.GenerateEmptyGrid(3, 3);
            Coordinate move = pacman.GetMove(grid);
            
            Assert.Equal(2, move.GetRow());            
            Assert.Equal(0, move.GetColumn());
        }

        [Fact]
        public void given_InputEqualsEast_when_GetMove_then_SymbolEqualsPacmanEastFacing()
        {
            IUserInput input = new TestInput(new List<string>{Constants.East});
            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), new Coordinate(11,9));
            Coordinate move = pacman.GetMove(_defaultGrid);
            
            Assert.Equal(DisplaySymbol.PacmanEastFacing, pacman.GetSymbol());
        }
        
        [Fact]
        public void given_InputEqualsNorth_when_GetMove_then_SymbolEqualsPacmanNorthFacing()
        {
            IUserInput input = new TestInput(new List<string>{Constants.North});
            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), new Coordinate(11,9));
            Coordinate move = pacman.GetMove(_defaultGrid);
            
            Assert.Equal(DisplaySymbol.PacmanNorthFacing, pacman.GetSymbol());
        }
        
        [Fact]
        public void given_InputEqualsNorth_when_MakeCharacterMove_then_GameStateGridIsUpdated()
        {
            Grid grid = _gridBuilder.GenerateEmptyGrid(19, 21);
            grid = _engine.PlacePacmanOnStartingPosition(grid, new Coordinate(11, 9));

            IUserInput input = new TestInput(new List<string>{Constants.North});
            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), new Coordinate(11,9));
            GameState gameState = _engine.MakeCharacterMove(grid, pacman);
            
            Assert.Equal(DisplaySymbol.PacmanNorthFacing, gameState.GetGrid().GetPoint(new Coordinate(10,9)));
        }
        
        [Fact]
        public void given_InputEqualsWest_when_MakeCharacterMove_then_GameStateGridIsUpdated()
        {
            Grid grid = _gridBuilder.GenerateEmptyGrid(19, 21);
            grid = _engine.PlacePacmanOnStartingPosition(grid, new Coordinate(11, 9));

            IUserInput input = new TestInput(new List<string>{Constants.West});
            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), new Coordinate(11,9));
            GameState gameState = _engine.MakeCharacterMove(grid, pacman);
            
            Assert.Equal(DisplaySymbol.PacmanWestFacing, gameState.GetGrid().GetPoint(new Coordinate(11,8)));
        }
        
        [Fact]
        public void given_PacmanIsInPositionNineEleven_when_MakeCharacterMove_then_NineElevenEqualsBlankSpace()
        {
            Grid grid = _gridBuilder.GenerateEmptyGrid(19, 21);
            grid = _engine.PlacePacmanOnStartingPosition(grid, new Coordinate(11, 9));

            IUserInput input = new TestInput(new List<string>{Constants.West});
            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), new Coordinate(11,9));
            GameState gameState = _engine.MakeCharacterMove(grid, pacman);
            
            Assert.Equal(DisplaySymbol.BlankSpace, gameState.GetGrid().GetPoint(new Coordinate(11,9)));
        }

        [Fact]
        public void given_OnlyPossibleMoveIsOneTwo_when_MakeCharacterMove_then_CoordinateOfPacmanEqualsOneTwo()
        {
            Grid grid = _gridBuilder.GenerateInitialGrid
            (3,
                3, 
                new List<Coordinate>(){new Coordinate(1,0), new Coordinate(2,1), new Coordinate(0,1)},
                new List<Coordinate>()
            );
            
            IUserInput input = new TestInput(new List<string>{Constants.West, Constants.North, Constants.East});
            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), new Coordinate(1,1));
            grid = _engine.PlacePacmanOnStartingPosition(grid, new Coordinate(1, 1));
            GameState gameState = _engine.MakeCharacterMove(grid, pacman);
            
            Assert.Equal(1, pacman.GetCoordinate().GetRow());
            Assert.Equal(2, pacman.GetCoordinate().GetColumn());
        }
    }
}