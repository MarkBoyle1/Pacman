using System;
using System.Collections.Generic;
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
            
            Assert.Equal(19, emptyGrid.Surface[0].Length);
        }
        
        [Fact]
        public void given_WallCoordinatesListContainsOneOne_when_AddWalls_then_OneOneContainsAWall()
        {
            Grid grid = _gridBuilder.GenerateEmptyGrid(19);

            grid = _gridBuilder.AddWalls(grid, new List<Coordinates> {new Coordinates(1, 1)});
            
            Assert.Equal(DisplaySymbol.Wall, grid.GetPoint(1,1));
        }
        
        [Fact]
        public void given_BlankSpacesCoordinatesListContainsOneOne_when_AddBlankSpaces_then_OneOneContainsBlankSpace()
        {
            Grid grid = _gridBuilder.GenerateEmptyGrid(19);

            grid = _gridBuilder.AddBlankSpaces(grid, new List<Coordinates> {new Coordinates(1, 1)});
            
            Assert.Equal(DisplaySymbol.BlankSpace, grid.GetPoint(1,1));
        }
    }
}