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
        public void when_GenerateEmptyGrid_and_WidthEqualsThree_then_lengthOfGridRowEqualsThree()
        {
            Grid emptyGrid = _gridBuilder.GenerateEmptyGrid(3, 5);
            
            Assert.Equal(3, emptyGrid.Surface[0].Length);
        }
        
        [Fact]
        public void given_WallCoordinatesListContainsOneOne_when_AddWalls_then_OneOneContainsWall()
        {
            Grid grid = _gridBuilder.GenerateEmptyGrid(3, 3);

            grid = _gridBuilder.AddWalls(grid, new List<Coordinate> {new Coordinate(1, 1)});
            
            Assert.Equal(DisplaySymbol.Wall, grid.GetPoint(new Coordinate(1,1)));
        }
        
        [Fact]
        public void given_BlankSpacesCoordinatesListContainsOneOne_when_AddBlankSpaces_then_OneOneContainsBlankSpace()
        {
            Grid grid = _gridBuilder.GenerateEmptyGrid(3, 3);

            grid = _gridBuilder.AddBlankSpaces(grid, new List<Coordinate> {new Coordinate(1, 1)});
            
            Assert.Equal(DisplaySymbol.BlankSpace, grid.GetPoint(new Coordinate(1,1)));
        }
        
        [Fact]
        public void given_BlankSpacesCoordinatesListContainsOneOne_when_GenerateInitialGrid_then_OneOneContainsBlankSpace()
        {
            ILayout layout = new TestLayout
            (
                new Coordinate(1, 1), 
                new List<Coordinate>(),
                new List<Coordinate>(){new Coordinate(2,2)}, 
                new List<Coordinate>(){new Coordinate(1,1)}
            );
            Grid grid = _gridBuilder.GenerateInitialGrid(layout);

            Assert.Equal(DisplaySymbol.Wall, grid.GetPoint(new Coordinate(2, 2)));
            Assert.Equal(DisplaySymbol.BlankSpace, grid.GetPoint(new Coordinate(1,1)));
        }
        
        [Fact]
        public void given_OriginalLayoutLevelContainsWallAtTwoTwo_when_GenerateInitialGrid_then_TwoTwoContainsWall()
        {
            ILayout layout = new OriginalLayout();
            Level level = new Level(1, layout);

            Grid grid = _gridBuilder.GenerateInitialGrid
            (
                layout
            );

            Assert.Equal(DisplaySymbol.Wall, grid.GetPoint(new Coordinate(2,2)));
        }

        [Fact]
        public void given_CoordinateEqualsOneOne_and_SymbolEqualsPacmanEastFacing_when_UpdateGrid_then_OneOneEqualsPacmanEastFacing()
        {
            Grid grid = _gridBuilder.GenerateEmptyGrid(19, 21);

            grid = _gridBuilder.UpdateGrid(grid, DisplaySymbol.PacmanEastFacing, new Coordinate(1,1));
            
            Assert.Equal(DisplaySymbol.PacmanEastFacing, grid.GetPoint(new Coordinate(1,1)));
        }
        
        [Fact]
        public void given_WidthEqualsThreeAndHeightEqualsThree_when_GenerateEmptyGrid_then_DotsRemainingEqualsNine()
        {
            Grid grid = _gridBuilder.GenerateEmptyGrid(3, 3);
            
            Assert.Equal(9, grid.GetDotsRemaining());
        }
    }
}