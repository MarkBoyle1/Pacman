using System.Collections.Generic;
using Pacman;
using Pacman.Input;
using Pacman.Output;
using Xunit;

namespace PacmanTests
{
    public class GameplayTests
    {
        private GridBuilder _gridBuilder;
        private string _testHighScoreFilePath;
        private string _testSavedGameFilePath;

        public GameplayTests()
        {
            _gridBuilder = new GridBuilder();
            _testHighScoreFilePath = "../../../../Pacman/TestHighScore.csv";
            _testSavedGameFilePath = "../../../../Pacman/TestSavedGame.json";
        }
        
        [Fact]
        public void given_PacmanMovesIntoDot_when_PlayOneTick_then_ScoreIncreasesByOne()
        {
            ILayout layout = new TestLayout
                (
                    new Coordinate(1, 1), 
                    new List<Coordinate>(), 
                    new List<Coordinate>(), 
                    new List<Coordinate>()
                );
            
            IUserInput input = new TestInput(new List<string>{Constants.East});
            Engine engine = new Engine(layout, input, new ConsoleOutput(), _testHighScoreFilePath);
            GameSetUp gameSetUp = new GameSetUp(new ConsoleOutput(), input, new Level(1, layout),
                _testSavedGameFilePath);
            Grid grid = _gridBuilder.GenerateInitialGrid(layout); 
            
            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), layout.GetPacmanStartingPosition());
            List<Character> characterList = new List<Character>() {pacman};
            grid = gameSetUp.PlaceCharactersOnGrid(grid, characterList);

            GameState gameState = new GameState(grid, 0, 1, 3, characterList);

            gameState = engine.PlayOneTick(gameState);
            
            Assert.Equal(1, gameState.Score);
        }
        
        [Fact]
        public void given_PacmanMovesEast_when_PlayOneTick_then_GameStateGridShowsTheUpdate()
        {
            ILayout layout = new TestLayout
                (
                new Coordinate(1, 1), 
                new List<Coordinate>(),
                new List<Coordinate>(), 
                new List<Coordinate>()
                );
            
            IUserInput input = new TestInput(new List<string>{Constants.East});
            Engine engine = new Engine(layout, input, new ConsoleOutput(), _testHighScoreFilePath);
            GameSetUp gameSetUp = new GameSetUp(new ConsoleOutput(), input, new Level(1, layout),
                _testSavedGameFilePath);
            Grid grid = _gridBuilder.GenerateInitialGrid(layout);
            
            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), layout.GetPacmanStartingPosition());
            List<Character> characterList = new List<Character>() {pacman};
            grid = gameSetUp.PlaceCharactersOnGrid(grid, characterList);

            GameState gameState = new GameState(grid, 0, 1, 3, characterList);

            gameState = engine.PlayOneTick(gameState);
            
            Assert.Equal(DisplaySymbol.PacmanEastFacing, gameState.Grid.GetPoint(new Coordinate(1,2)));
        }

        [Fact]
        public void given_DotsRemainingEqualsEight_and_PacmanEatsADot_when_PlayOneTick_then_DotsRemainingEqualsSeven()
        {
            ILayout layout = new TestLayout
            (
                new Coordinate(1, 1), 
                new List<Coordinate>(),
                new List<Coordinate>(), 
                new List<Coordinate>()
            );
            IUserInput input = new TestInput(new List<string>{Constants.East});
            Engine engine = new Engine(layout, input, new ConsoleOutput(), _testHighScoreFilePath);
            GameSetUp gameSetUp = new GameSetUp(new ConsoleOutput(), input, new Level(1, layout),
                _testSavedGameFilePath);
            Grid grid = _gridBuilder.GenerateInitialGrid(layout);
            
            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), layout.GetPacmanStartingPosition());
            List<Character> characterList = new List<Character>() {pacman};
            grid = gameSetUp.PlaceCharactersOnGrid(grid, characterList);

            GameState gameState = new GameState(grid, 0, 1, 3, characterList);

            gameState = engine.PlayOneTick(gameState);
            
            Assert.Equal(7, gameState.Grid.DotsRemaining);
        }

        [Fact]
        public void given_PacmanEatsTheLastDot_when_PlayOneLevel_then_LevelIncreasesByOne()
        {
            ILayout layout = new TestLayout
            (
                new Coordinate(0,0), 
                new List<Coordinate>(),
                new List<Coordinate>(), 
                new List<Coordinate>(),
                2,
                1
            );
            IUserInput input = new TestInput(new List<string>{Constants.East});
            Engine engine = new Engine(layout, input, new ConsoleOutput(), _testHighScoreFilePath);
            GameSetUp gameSetUp = new GameSetUp(new ConsoleOutput(), input, new Level(1, layout),
                _testSavedGameFilePath);
            Grid grid = _gridBuilder.GenerateEmptyGrid(2, 1);
            Level level = new Level(1, layout);

            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), layout.GetPacmanStartingPosition());
            List<Character> characterList = new List<Character>() {pacman};
            grid = gameSetUp.PlaceCharactersOnGrid(grid, characterList);

            GameState gameState = new GameState(grid, 0, 1, 3, characterList);

            gameState = engine.PlayOneLevel(gameState, level);
            
            Assert.Equal(2, gameState.LevelNumber);
        }
        
        [Fact]
        public void given_PacmanEatsTheLastDot_when_PlayOneLevel_then_LayoutIsReset()
        {
            ILayout layout = new TestLayout
            (
                new Coordinate(0,0), 
                new List<Coordinate>(),
                new List<Coordinate>(), 
                new List<Coordinate>(),
                2,
                1
            );
            IUserInput input = new TestInput(new List<string>{Constants.East});
            Engine engine = new Engine(layout, input, new ConsoleOutput(), _testHighScoreFilePath);
            Grid grid = _gridBuilder.GenerateEmptyGrid(2, 1);
            Level level = new Level(1, layout);
            GameSetUp gameSetUp = new GameSetUp(new ConsoleOutput(), input, level,
                _testSavedGameFilePath);
            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), layout.GetPacmanStartingPosition());
            List<Character> characterList = new List<Character>() {pacman};
            grid = gameSetUp.PlaceCharactersOnGrid(grid, characterList);

            GameState gameState = new GameState(grid, 0, 1, 3, characterList);

            engine.PlayOneLevel(gameState, level);
            
            Assert.Equal(0, pacman.GetCoordinate().GetRow());
            Assert.Equal(0, pacman.GetCoordinate().GetColumn());
        }

        [Fact]
        public void when_PlayOneTick_then_AllCharactersMove()
        {
            ILayout layout = new TestLayout
            (
                new Coordinate(1,1), 
                new List<Coordinate>()
                {
                    new Coordinate(3,3)
                },
                new List<Coordinate>()
                {
                    new Coordinate(3,4), 
                    new Coordinate(4,3), 
                    new Coordinate(2,3)
                }, 
                new List<Coordinate>(),
                5,
                5,
                1
            );
            
            IUserInput input = new TestInput(new List<string>() {Constants.North});
            Engine engine = new Engine(layout, input, new ConsoleOutput(), _testHighScoreFilePath);

            Grid grid = _gridBuilder.GenerateInitialGrid(layout);
            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), layout.GetPacmanStartingPosition());
            Character monster = new Monster(new Coordinate(3,3), true);
            List<Character> characters = new List<Character>() {pacman, monster};

            GameState gameState = new GameState(grid, 0, 1, 3, characters);

            gameState = engine.PlayOneTick(gameState);
            
            Assert.Equal(DisplaySymbol.BlankSpace, gameState.Grid.GetPoint(new Coordinate(1,1)));
            Assert.Equal(DisplaySymbol.PacmanNorthFacing, gameState.Grid.GetPoint(new Coordinate(0,1)));
            Assert.Equal(DisplaySymbol.Dot, gameState.Grid.GetPoint(new Coordinate(3,3)));
            Assert.Equal(DisplaySymbol.Monster, gameState.Grid.GetPoint(new Coordinate(3,2)));
        }

        [Fact]
        public void given_LivesLeftEqualsThree_when_PacmanDies_then_LivesLeftEqualsTwo()
        {
            ILayout layout = new TestLayout
            (
                new Coordinate(0,0), 
                new List<Coordinate>(),
                new List<Coordinate>(), 
                new List<Coordinate>()
                
            );
            Engine engine = new Engine(layout, new TestInput(new List<string>()), new ConsoleOutput(), _testHighScoreFilePath);

            Level level = new Level(1, layout);

            Grid grid = _gridBuilder.GenerateEmptyGrid(3, 3);
            Character pacman = new PacmanCharacter(new UserInput(), new ConsoleOutput(), layout.GetPacmanStartingPosition());
            List<Character> characters = new List<Character>() {pacman};

            GameState gameState = new GameState(grid, 0, 1, 3, characters);

            gameState = engine.UpdateGameStateForPacmanDeath(gameState, level);
            
            Assert.Equal(2, gameState.LivesLeft);
        }
        
        [Fact]
        public void when_PacmanDies_then_PacmanMovesToStartingLocation()
        {
            ILayout layout = new TestLayout
            (
                new Coordinate(1,1), 
                new List<Coordinate>(),
                new List<Coordinate>(), 
                new List<Coordinate>()
            );
            
            IUserInput input = new TestInput(new List<string>() {Constants.North});
            Engine engine = new Engine(layout, input, new ConsoleOutput(), _testHighScoreFilePath);

            Level level = new Level(1, layout);
            GameSetUp gameSetUp = new GameSetUp(new ConsoleOutput(), input, level,
                _testSavedGameFilePath);
            Grid grid = _gridBuilder.GenerateInitialGrid(layout);

            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), layout.GetPacmanStartingPosition());
            List<Character> characters = new List<Character>() {pacman};
            grid = gameSetUp.PlaceCharactersOnGrid(grid, characters);

            GameState gameState = new GameState(grid, 0, 1, 3, characters);
            gameState = engine.MoveCharacter(gameState, pacman);

            gameState = engine.UpdateGameStateForPacmanDeath(gameState, level);
            
            Assert.Equal(DisplaySymbol.DefaultPacmanStartingSymbol, gameState.Grid.GetPoint(new Coordinate(1,1)));
        }
        
        [Fact]
        public void given_PacmanMovesIntoMonster_when_MakeCharacterMove_then_LivesLeftDecreaseByOne()
        {
            ILayout layout = new TestLayout
            (
                new Coordinate(0,0), 
                new List<Coordinate>(){new Coordinate(0,1)},
                new List<Coordinate>(), 
                new List<Coordinate>(),
                3,
                3,
                1
            );
            IUserInput input = new TestInput(new List<string>() {Constants.East});
            Engine engine = new Engine(layout, input, new ConsoleOutput(), _testHighScoreFilePath);
            GameSetUp gameSetUp = new GameSetUp(new ConsoleOutput(), input, new Level(1, layout),
                _testSavedGameFilePath);
            Grid grid = _gridBuilder.GenerateInitialGrid(layout);
        
            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), layout.GetPacmanStartingPosition());
            Character monster = new Monster(new Coordinate(0, 1), true);
            List<Character> characters = new List<Character>() {pacman, monster};
            
            grid = gameSetUp.PlaceCharactersOnGrid(grid, characters);
        
            GameState gameState = new GameState(grid, 0, 1, 3, characters);
            gameState = engine.PlayOneTick(gameState);
        
            Assert.Equal(2, gameState.LivesLeft);
        }

        [Fact]
        public void given_MonsterMovesIntoPacman_when_MakeCharacterMove_then_LivesLeftDecreaseByOne()
        {
            ILayout layout = new TestLayout
            (
                new Coordinate(1,0), 
                new List<Coordinate>(){new Coordinate(0,1)},
                new List<Coordinate>()
                {
                    new Coordinate(0,0),
                    new Coordinate(0,2),
                    new Coordinate(2,1)
                }, 
                new List<Coordinate>(),
                3,
                3,
                1
                
            );
            IUserInput input = new TestInput(new List<string>() {Constants.East});
            Engine engine = new Engine(layout, input, new ConsoleOutput(), _testHighScoreFilePath);
            GameSetUp gameSetUp = new GameSetUp(new ConsoleOutput(), input, new Level(1, layout),
                _testSavedGameFilePath);
            Grid grid = _gridBuilder.GenerateInitialGrid(layout);
        
            Character pacman = new PacmanCharacter(input, new ConsoleOutput(), layout.GetPacmanStartingPosition());
            Character monster = new Monster(new Coordinate(0, 1), true);
            List<Character> characters = new List<Character>() {pacman, monster};
            
            grid = gameSetUp.PlaceCharactersOnGrid(grid, characters);
        
            GameState gameState = new GameState(grid, 0, 1, 3, characters);
            gameState = engine.PlayOneTick(gameState);
        
            Assert.Equal(2, gameState.LivesLeft);
        }

        [Fact]
        public void
            given_GameScoreEqualsThree_and_HighScoreEqualsTwo_when_UpdateHighScoreIfRequired_then_return_Three()
        {
            ILayout layout = new TestLayout
            (
                new Coordinate(0,0), 
                new List<Coordinate>(),
                new List<Coordinate>(), 
                new List<Coordinate>()
                
            );
            Engine engine = new Engine(layout, new TestInput(new List<string>()), new ConsoleOutput(), _testHighScoreFilePath);
            
            int gameScore = 3;
            int highScore = 2;

            highScore = engine.UpdateHighScoreIfRequired(gameScore, highScore);
            
            Assert.Equal(3, highScore);
        }
        
        [Fact]
        public void
            given_GameScoreEqualsOne_and_HighScoreEqualsTwo_when_UpdateHighScoreIfRequired_then_return_Two()
        {
            ILayout layout = new TestLayout
            (
                new Coordinate(0,0), 
                new List<Coordinate>(),
                new List<Coordinate>(), 
                new List<Coordinate>()
                
            );
            Engine engine = new Engine(layout, new TestInput(new List<string>()), new ConsoleOutput(), _testHighScoreFilePath);
            
            int gameScore = 1;
            int highScore = 2;

            highScore = engine.UpdateHighScoreIfRequired(gameScore, highScore);
            
            Assert.Equal(2, highScore);
        }
        
        [Fact]
        public void
            given_SavedGameFileScoreEqualsThirteen_when_LoadSavedGame_then_GameStateScoreEqualsThirteen()
        {
            ILayout layout = new TestLayout
            (
                new Coordinate(0,0), 
                new List<Coordinate>(),
                new List<Coordinate>(), 
                new List<Coordinate>()
                
            );
            
            GameSetUp gameSetUp = new GameSetUp(new ConsoleOutput(), new UserInput(), new Level(1, layout),
                _testSavedGameFilePath);
            GameState gameState = gameSetUp.LoadPreviousGame();
            
            Assert.Equal(13, gameState.Score);
        }
        
        [Fact]
        public void
            given_SavedGameFileLivesLeftEqualsTwo_when_LoadSavedGame_then_GameStateLivesLeftEqualsTwo()
        {
            ILayout layout = new TestLayout
            (
                new Coordinate(0,0), 
                new List<Coordinate>(),
                new List<Coordinate>(), 
                new List<Coordinate>()
                
            );
            
            GameSetUp gameSetUp = new GameSetUp(new ConsoleOutput(), new UserInput(), new Level(1, layout),
                _testSavedGameFilePath);
            GameState gameState = gameSetUp.LoadPreviousGame();
            
            Assert.Equal(2, gameState.LivesLeft);
        }
        
        [Fact]
        public void
            given_SavedGameFileLevelEqualsOne_when_LoadSavedGame_then_GameStateLevelEqualsOne()
        {
            ILayout layout = new TestLayout
            (
                new Coordinate(0,0), 
                new List<Coordinate>(),
                new List<Coordinate>(), 
                new List<Coordinate>()
            );
            
            GameSetUp gameSetUp = new GameSetUp(new ConsoleOutput(), new UserInput(), new Level(1, layout),
                _testSavedGameFilePath);
            GameState gameState = gameSetUp.LoadPreviousGame();
            
            Assert.Equal(1, gameState.LevelNumber);
        }
    }
}