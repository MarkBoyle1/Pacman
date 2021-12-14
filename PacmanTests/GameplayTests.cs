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
        public void IncreasesScoreWhenDotIsEaten()
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
            _engine.MakeCharacterMove(grid, pacman);

            GameState gameState = _engine.PlayOneTick();
            
            
            Assert.Equal(1, gameState.GetScore());
        }
    }
}