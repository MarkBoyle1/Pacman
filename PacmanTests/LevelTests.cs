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
            ILayout layout = new TestLayout
            (
                new Coordinate(0, 0), 
                new List<Coordinate>(),
                new List<Coordinate>(), 
                new List<Coordinate>(),
                3,
                3,
                4
            );
            Level level = new Level(2, layout);
            
            Assert.Equal(6, level.GetMonsters().Count);
        }
    }
}