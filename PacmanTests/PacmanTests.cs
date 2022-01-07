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
        private Grid _defaultGrid;
        private string _testHighScoreFilePath = "../../../../Pacman/TestHighScore.csv";

        public PacmanTests()
        {
            _gridBuilder = new GridBuilder();
            _defaultGrid = _gridBuilder.GenerateEmptyGrid(19, 21);
        }
        
        [Fact]
        public void given_StartingLocationEqualsNineEleven_when_PlacePacmanOnStartingLocation_then_NineElevenEqualsPacman()
        {
            ILayout layout = new TestLayout
            (
                new Coordinate(1, 1), 
                new List<Coordinate>(),
                new List<Coordinate>(), 
                new List<Coordinate>()
            );
            Engine engine = new Engine(layout, new TestInput(new List<string>()), new ConsoleOutput(), _testHighScoreFilePath);

            Grid grid = _gridBuilder.GenerateEmptyGrid(3,3);
            Character pacman =
                new PacmanCharacter(new UserInput(), new ConsoleOutput(), layout.GetPacmanStartingPosition());
            List<Character> characterList = new List<Character>() {pacman};
        
            grid = engine.PlaceCharactersOnGrid(grid, characterList);
            
            Assert.Equal(DisplaySymbol.DefaultPacmanStartingSymbol, grid.GetPoint(new Coordinate(1,1)));
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
            
            pacman.GetMove(_defaultGrid);
            
            Assert.Equal(DisplaySymbol.PacmanEastFacing, pacman.GetSymbol());
        }
        
        [Fact]
        public void given_InputEqualsNorth_when_GetMove_then_SymbolEqualsPacmanNorthFacing()
        {
            IUserInput input = new TestInput(new List<string>{Constants.North});
            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), new Coordinate(11,9));
            
            pacman.GetMove(_defaultGrid);
            
            Assert.Equal(DisplaySymbol.PacmanNorthFacing, pacman.GetSymbol());
        }
        
        [Fact]
        public void given_InputEqualsNorth_when_MakeCharacterMove_then_GameStateGridIsUpdated()
        {
            ILayout layout = new TestLayout
            (
                new Coordinate(11,9), 
                new List<Coordinate>(),
                new List<Coordinate>(), 
                new List<Coordinate>(),
                19,
                21
            );
            IUserInput input = new TestInput(new List<string>{Constants.North});
            Engine engine = new Engine(layout, input, new ConsoleOutput(), _testHighScoreFilePath);

            Grid grid = _gridBuilder.GenerateEmptyGrid(19, 21);
            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), layout.GetPacmanStartingPosition());
            List<Character> characterList = new List<Character>() {pacman};
            grid = engine.PlaceCharactersOnGrid(grid, characterList);
            GameState gameState = new GameState(grid, 0, 1, 3, characterList);

            gameState = engine.MakeCharacterMove(gameState, pacman);
            
            Assert.Equal(DisplaySymbol.PacmanNorthFacing, gameState.GetGrid().GetPoint(new Coordinate(10,9)));
        }
        
        [Fact]
        public void given_InputEqualsWest_when_MakeCharacterMove_then_GameStateGridIsUpdated()
        {
            ILayout layout = new TestLayout
            (
                new Coordinate(11,9), 
                new List<Coordinate>(),
                new List<Coordinate>(), 
                new List<Coordinate>(),
                19,
                21
            );
            IUserInput input = new TestInput(new List<string>{Constants.West});
            Engine engine = new Engine(layout, input, new ConsoleOutput(), _testHighScoreFilePath);

            Grid grid = _gridBuilder.GenerateEmptyGrid(19, 21);

            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), new Coordinate(11,9));
            List<Character> characterList = new List<Character>() {pacman};

            grid = engine.PlaceCharactersOnGrid(grid, characterList);
            GameState gameState = new GameState(grid, 0, 1, 3, characterList);

            gameState = engine.MakeCharacterMove(gameState, pacman);
            
            Assert.Equal(DisplaySymbol.PacmanWestFacing, gameState.GetGrid().GetPoint(new Coordinate(11,8)));
        }
        
        [Fact]
        public void given_PacmanIsInPositionNineEleven_when_MakeCharacterMove_then_NineElevenEqualsBlankSpace()
        {
            ILayout layout = new TestLayout
            (
                new Coordinate(11,9), 
                new List<Coordinate>(),
                new List<Coordinate>(), 
                new List<Coordinate>(),
                19,
                21
            );
            IUserInput input = new TestInput(new List<string>{Constants.West});
            Engine engine = new Engine(layout, input, new ConsoleOutput(), _testHighScoreFilePath);

            Grid grid = _gridBuilder.GenerateEmptyGrid(19, 21);

            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), new Coordinate(11,9));
            List<Character> characterList = new List<Character>() {pacman};

            grid = engine.PlaceCharactersOnGrid(grid, characterList);
            GameState gameState = new GameState(grid, 0, 1, 3, characterList);

            gameState = engine.MakeCharacterMove(gameState, pacman);
            
            Assert.Equal(DisplaySymbol.BlankSpace, gameState.GetGrid().GetPoint(new Coordinate(11,9)));
        }

        [Fact]
        public void given_OnlyPossibleMoveIsOneTwo_when_MakeCharacterMove_then_CoordinateOfPacmanEqualsOneTwo()
        {
            ILayout layout = new TestLayout
            (
                new Coordinate(1,1), 
                new List<Coordinate>(),
                new List<Coordinate>()
                {
                    new Coordinate(0,1), 
                    new Coordinate(2,1), 
                    new Coordinate(1,0)
                }, 
                new List<Coordinate>(),
                19,
                21
            );
            IUserInput input = new TestInput(new List<string>{Constants.West, Constants.North, Constants.East});
            Engine engine = new Engine(layout, input, new ConsoleOutput(), _testHighScoreFilePath);

            Grid grid = _gridBuilder.GenerateInitialGrid(layout);
            
            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), new Coordinate(1,1));
            List<Character> characterList = new List<Character>() {pacman};

            grid = engine.PlaceCharactersOnGrid(grid, characterList);
            GameState gameState = new GameState(grid, 0, 1, 3, characterList);

            engine.MakeCharacterMove(gameState, pacman);
            
            Assert.Equal(1, pacman.GetCoordinate().GetRow());
            Assert.Equal(2, pacman.GetCoordinate().GetColumn());
        }
    }
}