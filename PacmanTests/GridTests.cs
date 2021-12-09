using System;
using Pacman;
using Xunit;

namespace PacmanTests
{
    public class GridTests
    {
        private GridBuilder _gridBuilder = new GridBuilder();
        
        [Fact]
        public void when_GenerateEmptyGrid_and_sizeOfGridEquals19_then_lengthOfGridRowEquals19()
        {
            Grid emptyGrid = _gridBuilder.GenerateEmptyGrid(19);
            
            Assert.Equal(19, emptyGrid.grid[0].Length);
        }
    }
}