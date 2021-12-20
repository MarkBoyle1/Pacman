using System.Collections.Generic;
using Pacman;
using Xunit;

namespace PacmanTests
{
    public class LevelTests
    {
        [Fact]
        public void given_layoutStartsWithFourMonsters_and_levelEqualsTwo_then_MonstersListContainsSixMonsters()
        {
            Level level = new Level(2, new TestLayout(4, new Coordinate(0,0), new List<Coordinate>()));
            
            Assert.Equal(6, level.GetMonsters().Count);
        }
    }
}