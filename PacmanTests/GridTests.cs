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
            Grid emptyGrid = _gridBuilder.GenerateEmptyGrid(19, 21);
            
            Assert.Equal(19, emptyGrid.Surface[0].Length);
        }
        
        [Fact]
        public void given_WallCoordinatesListContainsOneOne_when_AddWalls_then_OneOneContainsWall()
        {
            Grid grid = _gridBuilder.GenerateEmptyGrid(19, 21);

            grid = _gridBuilder.AddWalls(grid, new List<Coordinate> {new Coordinate(1, 1)});
            
            Assert.Equal(DisplaySymbol.Wall, grid.GetPoint(new Coordinate(1,1)));
        }
        
        [Fact]
        public void given_BlankSpacesCoordinatesListContainsOneOne_when_AddBlankSpaces_then_OneOneContainsBlankSpace()
        {
            Grid grid = _gridBuilder.GenerateEmptyGrid(19, 21);

            grid = _gridBuilder.AddBlankSpaces(grid, new List<Coordinate> {new Coordinate(1, 1)});
            
            Assert.Equal(DisplaySymbol.BlankSpace, grid.GetPoint(new Coordinate(1,1)));
        }
        
        [Fact]
        public void given_BlankSpacesCoordinatesListContainsOneOne_when_GenerateInitialGrid_then_OneOneContainsBlankSpace()
        {
            Grid grid = _gridBuilder.GenerateInitialGrid
                (
                    19, 
                    21,
                    new List<Coordinate>(){new Coordinate(2,2)}, 
                    new List<Coordinate>(){new Coordinate(1,1)}
                );

            Assert.Equal(DisplaySymbol.Wall, grid.GetPoint(new Coordinate(2, 2)));
            Assert.Equal(DisplaySymbol.BlankSpace, grid.GetPoint(new Coordinate(1,1)));
        }
        
        [Fact]
        public void given_OriginalLayoutLevelContainsWallAtTwoTwo_when_GenerateInitialGrid_then_TwoTwoContainsWall()
        {
            ILevel level = new OriginalLayoutLevel();

            Grid grid = _gridBuilder.GenerateInitialGrid
            (
                level.GetGridWidth(),
                level.GetGridHeight(),
                level.GetWallCoordinates(), 
                level.GetBlankSpacesCoordinates()
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
        public void given_CoordinateEqualsOneOne_and_WallsOnEachSideExceptOneTwo_when_GetPossibleMoves_then_return_ListWithOneTwo()
        {
            Grid grid = _gridBuilder.GenerateInitialGrid
                (3,
                    3, 
                    new List<Coordinate>(){new Coordinate(1,0), new Coordinate(2,1), new Coordinate(0,1)},
                new List<Coordinate>()
                );

            List<Coordinate> possibleMoves = grid.GetPossibleMoves(new Coordinate(1,1));
            
            Assert.Single(possibleMoves);
            Assert.Equal(1, possibleMoves[0].GetRow());
            Assert.Equal(2, possibleMoves[0].GetColumn());
        }
        
        [Fact]
        public void given_CoordinateEqualsOneOne_and_WallsOnTwoSides_when_GetPossibleMoves_then_return_ListWithTwoCoordinates()
        {
            Grid grid = _gridBuilder.GenerateInitialGrid
            (3,
                3, 
                new List<Coordinate>(){new Coordinate(1,0), new Coordinate(2,1)},
                new List<Coordinate>()
            );

            List<Coordinate> possibleMoves = grid.GetPossibleMoves(new Coordinate(1,1));
            
            Assert.Equal(2, possibleMoves.Count);
        }
        
        [Fact]
        public void given_WidthEqualsThreeAndHeightEqualsThree_when_GenerateEmptyGrid_then_DotsRemainingEqualsNine()
        {
            Grid grid = _gridBuilder.GenerateEmptyGrid(3, 3);
            
            Assert.Equal(9, grid.GetDotsRemaining());
        }
    }
}