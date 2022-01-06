using System.Collections.Generic;
using Pacman;
using Pacman.Input;
using Pacman.Output;
using Xunit;

namespace PacmanTests
{
    public class MonsterTests
    {
        private GridBuilder _gridBuilder = new GridBuilder();
        private string _testHighScoreFilePath = "../../../../Pacman/TestHighScore.csv";

        
        [Fact]
        public void given_levelHasOneMonster_when_GetMonsters_then_returns_ListOfOneMonster()
        {
            ILayout layout = new TestLayout
            (
                new Coordinate(1, 1), 
                new List<Coordinate>(){new Coordinate(0,1)},
                new List<Coordinate>(), 
                new List<Coordinate>(),
                3,
                3,
                1
            );
            Level level = new Level(1, layout);

            List<Character> monsterList = level.GetMonsters();
            
            Assert.Single(monsterList);
        }
        
        [Fact]
        public void given_levelHasOneMonsterAtZeroOne_when_PlaceCharactersOnGrid_then_ZeroOneEqualsMonster()
        {
            ILayout layout = new TestLayout
            (
                new Coordinate(1, 1), 
                new List<Coordinate>(){new Coordinate(0,1)},
                new List<Coordinate>(), 
                new List<Coordinate>(),
                3,
                3,
                1
            );
            Engine _engine = new Engine(layout, new TestInput(new List<string>()), new ConsoleOutput(), _testHighScoreFilePath);

           
            Grid grid = _gridBuilder.GenerateEmptyGrid(3, 3);
            Character monster = new Monster(new Coordinate(0, 1), true);
        
            List<Character> monsterList = new List<Character>(){monster};
        
            grid = _engine.PlaceCharactersOnGrid(grid, monsterList);
            
            Assert.Equal(DisplaySymbol.Monster, grid.GetPoint(new Coordinate(0,1)));
        }

        [Fact]
        public void given_OnlyPossibleMoveEqualsOneTwo_when_GetMove_then_returns_OneTwo()
        {
            ILayout layout = new TestLayout
            (
                new Coordinate(1, 1), 
                new List<Coordinate>(),
                new List<Coordinate>(){new Coordinate(1,0), new Coordinate(2,1), new Coordinate(0,1)}, 
                new List<Coordinate>()
            );
            Grid grid = _gridBuilder.GenerateInitialGrid
            (layout
            );

            Character monster = new Monster(new Coordinate(1,1), true);

            Coordinate coordinate = monster.GetMove(grid);
            
            Assert.Equal(1, coordinate.GetRow());
            Assert.Equal(2, coordinate.GetColumn());
        }
        
        [Fact]
        public void given_MonsterNeedsToWrapAroundGrid__when_GetMove_then_MonsterWrapsAroundGrid()
        {
            ILayout layout = new TestLayout
            (
                new Coordinate(1, 1), 
                new List<Coordinate>(),
                new List<Coordinate>(){new Coordinate(0,1), new Coordinate(0,2), new Coordinate(1,0)}, 
                new List<Coordinate>()
            );
            Grid grid = _gridBuilder.GenerateInitialGrid
            (layout
            );
            Character monster = new Monster(new Coordinate(0, 0), true);
            
            Coordinate move = monster.GetMove(grid);
            
            Assert.Equal(2, move.GetRow());            
            Assert.Equal(0, move.GetColumn());
        }
        
        [Fact]
        public void given_MonsterIsCurrentlyOverADot__when_MakeCharacterMove_then_DotRemainsThereAfterMonsterHasLeft()
        {
            ILayout layout = new TestLayout
            (
                new Coordinate(1, 1), 
                new List<Coordinate>(),
                new List<Coordinate>(){new Coordinate(0,0)}, 
                new List<Coordinate>(),
                3,
                3,
                1
            );
            Engine _engine = new Engine(layout, new TestInput(new List<string>()), new ConsoleOutput(), _testHighScoreFilePath);


            Grid grid = _gridBuilder.GenerateEmptyGrid(3, 3);
            Character monster = new Monster(new Coordinate(0, 0), true);
            
            List<Character> characterList = new List<Character>() {monster};

            grid = _engine.PlaceCharactersOnGrid(grid, characterList);
            GameState gameState = new GameState(grid, 0, 1, 3, characterList);

            gameState = _engine.MakeCharacterMove(gameState, monster);
            
            Assert.Equal(DisplaySymbol.Dot, gameState.GetGrid().GetPoint(new Coordinate(0,0)));
        }
        
        [Fact]
        public void given_MonsterIsNotCurrentlyOverADot__when_MakeCharacterMove_then_BlankSpaceRemainsThereAfterMonsterHasLeft()
        {
            ILayout layout = new TestLayout
            (
                new Coordinate(1, 1), 
                new List<Coordinate>(),
                new List<Coordinate>(){new Coordinate(0,1), new Coordinate(0,2), new Coordinate(1,0)}, 
                new List<Coordinate>(){new Coordinate(0,0)},
                3,
                3,
                1
            );
            Engine _engine = new Engine(layout, new TestInput(new List<string>()), new ConsoleOutput(), _testHighScoreFilePath);


            Grid grid = _gridBuilder.GenerateInitialGrid
            (layout
            );
            Character monster = new Monster(new Coordinate(0, 0), false);
            
            List<Character> characterList = new List<Character>() {monster};

            grid = _engine.PlaceCharactersOnGrid(grid, characterList);
            GameState gameState = new GameState(grid, 0, 1, 3, characterList);

            gameState = _engine.MakeCharacterMove(gameState, monster);
            
            Assert.Equal(DisplaySymbol.BlankSpace, gameState.GetGrid().GetPoint(new Coordinate(0,0)));
        }

        [Fact]
        public void MonstersDontBumpIntoEachOther()
        {
            ILayout layout = new TestLayout
            (
                new Coordinate(1, 1), 
                new List<Coordinate>(),
                new List<Coordinate>(){new Coordinate(0,1), new Coordinate(2,1)}, 
                new List<Coordinate>(){new Coordinate(0,0)},
                3,
                3,
                1
            );
            Engine _engine = new Engine(layout, new TestInput(new List<string>()), new ConsoleOutput(), _testHighScoreFilePath);


            Grid grid = _gridBuilder.GenerateInitialGrid
            (layout
            );
            Character monster1 = new Monster(new Coordinate(1, 1), false);
            Character monster2 = new Monster(new Coordinate(1, 0), false);
            List<Character> characterList = new List<Character>() {monster1, monster2};
            grid = _engine.PlaceCharactersOnGrid(grid, characterList);
            GameState gameState = new GameState(grid, 0, 1, 3, characterList);

            gameState = _engine.MakeCharacterMove(gameState, monster1);
            
            Assert.Equal(1, monster1.Coordinate.GetRow());
            Assert.Equal(2, monster1.Coordinate.GetColumn());
        }
        
        [Fact]
        public void given_NoPossibleMoves_when_MakeCharacterMove_then_return_currentCoordinate()
        {
            ILayout layout = new TestLayout
            (
                new Coordinate(1, 1), 
                new List<Coordinate>(){new Coordinate(1,1), new Coordinate(1,0)},
                new List<Coordinate>(){new Coordinate(0,1), new Coordinate(2,1), new Coordinate(1,2)}, 
                new List<Coordinate>(){new Coordinate(0,0)},
                3,
                3,
                2
            );
            Engine _engine = new Engine(layout, new TestInput(new List<string>()), new ConsoleOutput(), _testHighScoreFilePath);
            Grid grid = _gridBuilder.GenerateInitialGrid
            (layout
            );
            Character monster1 = new Monster(new Coordinate(1, 1), false);
            Character monster2 = new Monster(new Coordinate(1, 0), false);
            List<Character> characterList = new List<Character>() {monster1, monster2};
            grid = _engine.PlaceCharactersOnGrid(grid, characterList);
            GameState gameState = new GameState(grid, 0, 1, 3, characterList);

            gameState = _engine.MakeCharacterMove(gameState, monster1);
            
            Assert.Equal(1, monster1.Coordinate.GetRow());
            Assert.Equal(1, monster1.Coordinate.GetColumn());
        }
    }
}