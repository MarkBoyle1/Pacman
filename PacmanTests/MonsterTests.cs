using System.Collections.Generic;
using Pacman;
using Pacman.Input;
using Pacman.Output;
using Xunit;

namespace PacmanTests
{
    public class MonsterTests
    {
        private Engine _engine = new Engine();
        private GridBuilder _gridBuilder = new GridBuilder();
        
        [Fact]
        public void given_levelHasOneMonster_when_GetMonsters_then_returns_ListOfOneMonster()
        {
            ILevel level = new TestLevel(1);

            List<Character> monsterList = level.GetMonsters();
            
            Assert.Single(monsterList);
        }
        
        [Fact]
        public void given_levelHasOneMonsterAtZeroOne_when_PlaceCharactersOnGrid_then_ZeroOneEqualsMonster()
        {
            ILevel level = new TestLevel(1);
            Grid grid = _gridBuilder.GenerateEmptyGrid(3, 3);

            List<Character> monsterList = level.GetMonsters();

            grid = _engine.PlaceCharactersOnGrid(grid, monsterList);
            
            Assert.Equal(DisplaySymbol.Monster, grid.GetPoint(new Coordinate(0,1)));
        }
    }
}