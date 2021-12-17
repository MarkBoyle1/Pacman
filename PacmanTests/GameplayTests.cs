using System.Collections.Generic;
using Pacman;
using Pacman.Input;
using Pacman.Output;
using Xunit;

namespace PacmanTests
{
    public class GameplayTests
    {
        private GridBuilder _gridBuilder = new GridBuilder();
        private Engine _engine = new Engine();

        [Fact]
        public void given_PacmanMovesIntoDot_when_PlayOneTick_then_GameStateScoreIncreasesByOne()
        {
            Grid grid = _gridBuilder.GenerateInitialGrid
            (3,
                3, 
                new List<Coordinate>(){new Coordinate(1,0), new Coordinate(2,1), new Coordinate(0,1)},
                new List<Coordinate>()
            ); 
            
            IUserInput input = new TestInput(new List<string>{Constants.East});
            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), new Coordinate(1,1));
            List<Character> characterList = new List<Character>() {pacman};
            grid = _engine.PlaceCharactersOnGrid(grid, characterList);

            GameState gameState = new GameState(grid, 0, 1, characterList);

            gameState = _engine.PlayOneTick(gameState);
            
            Assert.Equal(1, gameState.GetScore());
        }
        
        [Fact]
        public void given_PacmanMovesEast_when_PlayOneTick_then_GameStateGridShowsTheUpdate()
        {
            Grid grid = _gridBuilder.GenerateEmptyGrid(3, 3);
            
            IUserInput input = new TestInput(new List<string>{Constants.East});
            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), new Coordinate(1,1));
            List<Character> characterList = new List<Character>() {pacman};
            grid = _engine.PlaceCharactersOnGrid(grid, characterList);

            GameState gameState = new GameState(grid, 0, 1, characterList);

            gameState = _engine.PlayOneTick(gameState);
            
            Assert.Equal(DisplaySymbol.PacmanEastFacing, gameState.GetGrid().GetPoint(new Coordinate(1,2)));
        }
        
        [Fact]
        public void given_PacmanIsTheOnlyCharacterOnTheGrid_when_PlayOneTick_then_GameStateCharacterListCountEqualsOne()
        {
            Grid grid = _gridBuilder.GenerateEmptyGrid(3, 3);
            
            IUserInput input = new TestInput(new List<string>{Constants.East});
            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), new Coordinate(1,1));
            List<Character> characterList = new List<Character>() {pacman};
            grid = _engine.PlaceCharactersOnGrid(grid, characterList);

            GameState gameState = new GameState(grid, 0, 1,characterList);

            gameState = _engine.PlayOneTick(gameState);
            
            Assert.Single(gameState.GetCharacterList());
        }

        [Fact]
        public void given_DotsRemainingEqualsEight_and_PacmanEatsADot_when_PlayOneTick_then_DotsRemainingEqualsSeven()
        {
            Grid grid = _gridBuilder.GenerateEmptyGrid(3, 3);
            
            IUserInput input = new TestInput(new List<string>{Constants.East});
            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), new Coordinate(1,1));
            List<Character> characterList = new List<Character>() {pacman};
            grid = _engine.PlaceCharactersOnGrid(grid, characterList);

            GameState gameState = new GameState(grid, 0, 1,characterList);

            gameState = _engine.PlayOneTick(gameState);
            
            Assert.Equal(7, gameState.GetGrid().GetDotsRemaining());
        }

        [Fact]
        public void given_PacmanEatsTheLastDot_when_PlayOneLevel_then_LevelIncreasesByOne()
        {
            Grid grid = _gridBuilder.GenerateEmptyGrid(2, 1);
            ILevel level = new TestLevel(0, new Coordinate(0,0), new List<Coordinate>());

            IUserInput input = new TestInput(new List<string>{Constants.East});
            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), new Coordinate(0,0));
            List<Character> characterList = new List<Character>() {pacman};
            grid = _engine.PlaceCharactersOnGrid(grid, characterList);

            GameState gameState = new GameState(grid, 0, 1, characterList);

            gameState = _engine.PlayOneLevel(gameState, level);
            
            Assert.Equal(2, gameState.GetLevel());
        }
        
        [Fact]
        public void given_PacmanEatsTheLastDot_when_PlayOneLevel_then_LayoutIsReset()
        {
            Grid grid = _gridBuilder.GenerateEmptyGrid(2, 1);
            ILevel level = new TestLevel(0, new Coordinate(0,0), new List<Coordinate>());
            
            IUserInput input = new TestInput(new List<string>{Constants.East});
            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), new Coordinate(0,0));
            List<Character> characterList = new List<Character>() {pacman};
            grid = _engine.PlaceCharactersOnGrid(grid, characterList);

            GameState gameState = new GameState(grid, 0, 1, characterList);

            gameState = _engine.PlayOneLevel(gameState, level);
            
            Assert.Equal(1, gameState.GetGrid().GetDotsRemaining());
            Assert.Equal(0, pacman.GetCoordinate().GetRow());
            Assert.Equal(0, pacman.GetCoordinate().GetColumn());
        }

        [Fact]
        public void when_PlayOneTick_then_AllCharactersMove()
        {
            Grid grid = _gridBuilder.GenerateEmptyGrid(5, 5);
            IUserInput input = new TestInput(new List<string>() {Constants.North});
            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), new Coordinate(1, 1));
            Character monster = new Monster(new Coordinate(3,3), true);
            List<Character> characters = new List<Character>() {pacman, monster};

            GameState gameState = new GameState(grid, 0, 1, characters);

            gameState = _engine.PlayOneTick(gameState);
            
            Assert.Equal(DisplaySymbol.BlankSpace, gameState.GetGrid().GetPoint(new Coordinate(1,1)));
            Assert.Equal(DisplaySymbol.Dot, gameState.GetGrid().GetPoint(new Coordinate(3,3)));
        }
    }
}