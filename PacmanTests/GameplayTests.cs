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
        public void given_PacmanMovesIntoADot_when_MakeCharacterMove_then_GameStateScoreIncreasesByOne()
        {
            Grid grid = _gridBuilder.GenerateInitialGrid
            (3,
                3, 
                new List<Coordinate>(){new Coordinate(1,0), new Coordinate(2,1), new Coordinate(0,1)},
                new List<Coordinate>()
            ); 
            
            IUserInput input = new TestInput(new List<string>{Constants.East});
            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), new Coordinate(1,1));
            grid = _engine.PlacePacmanOnStartingPosition(grid, new Coordinate(1, 1));
            List<Character> characterList = new List<Character>() {pacman};

            GameState gameState = new GameState(grid, 0, characterList);

            gameState = _engine.PlayOneTick(gameState);
            
            Assert.Equal(1, gameState.GetScore());
        }
        
        [Fact]
        public void given_PacmanMovesEast_when_PlayOneTick_then_GameStateGridShowsTheUpdate()
        {
            Grid grid = _gridBuilder.GenerateEmptyGrid(3, 3);
            
            IUserInput input = new TestInput(new List<string>{Constants.East});
            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), new Coordinate(1,1));
            grid = _engine.PlacePacmanOnStartingPosition(grid, new Coordinate(1, 1));
            List<Character> characterList = new List<Character>() {pacman};

            GameState gameState = new GameState(grid, 0, characterList);

            gameState = _engine.PlayOneTick(gameState);
            
            Assert.Equal(DisplaySymbol.PacmanEastFacing, gameState.GetGrid().GetPoint(new Coordinate(1,2)));
        }
        
        [Fact]
        public void given_PacmanIsTheOnlyCharacterOnTheGrid_when_PlayOneTick_then_GameStateCharacterListContainsone()
        {
            Grid grid = _gridBuilder.GenerateEmptyGrid(3, 3);
            
            IUserInput input = new TestInput(new List<string>{Constants.East});
            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), new Coordinate(1,1));
            grid = _engine.PlacePacmanOnStartingPosition(grid, new Coordinate(1, 1));
            List<Character> characterList = new List<Character>() {pacman};

            GameState gameState = new GameState(grid, 0, characterList);

            gameState = _engine.PlayOneTick(gameState);
            
            Assert.Single(gameState.GetCharacterList());
        }
    }
}