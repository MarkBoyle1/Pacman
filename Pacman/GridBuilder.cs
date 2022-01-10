using System.Collections.Generic;
using System.Linq;

namespace Pacman
{
    public class GridBuilder
    {
        public Grid GenerateInitialGrid(ILayout layout)
        {
            int width = layout.GetGridWidth();
            int height = layout.GetGridHeight();
            List<Coordinate> wallCoordinates = layout.GetWallCoordinates();
            List<Coordinate> blankSpacesCoordinates = layout.GetBlankSpacesCoordinates();
                
            Grid grid = GenerateEmptyGrid(width, height);
            grid = AddWalls(grid, wallCoordinates);
            grid = AddBlankSpaces(grid, blankSpacesCoordinates);
            int dotsRemaining = layout.GetStartingNumberOfDots();

            return new Grid(grid.Surface, dotsRemaining);
        }
        public Grid GenerateEmptyGrid(int width, int height)
        {
            string[][] emptyGrid = new string[height][];

            emptyGrid = emptyGrid.Select(x => new string[width].Select(x => DisplaySymbol.Dot).ToArray()).ToArray();

            int dotsRemaining = width * height;
            
            return new Grid(emptyGrid, dotsRemaining);
        }

        public Grid AddWalls(Grid grid, List<Coordinate> coordinates)
        {
            foreach (var coordinate in coordinates)
            {
                grid.Surface[coordinate.GetRow()][coordinate.GetColumn()] = DisplaySymbol.Wall;
            }
            
            return grid;
        }
        
        public Grid AddBlankSpaces(Grid grid, List<Coordinate> coordinates)
        {
            foreach (var coordinate in coordinates)
            {
                grid.Surface[coordinate.GetRow()][coordinate.GetColumn()] = DisplaySymbol.BlankSpace;
            }
            
            return grid;
        }

        public Grid UpdateGrid(Grid grid, string symbol, Coordinate coordinate)
        {
            string[][] updatedGrid = new string[grid.GetHeight()][];
            int dotsRemaining = 0;
            
            updatedGrid = updatedGrid.Select(x => new string[grid.GetWidth()]).ToArray();
                
            for(int row = 0; row < grid.GetHeight(); row++)
            {
                for (int column = 0; column < grid.GetWidth(); column++)
                {
                    string currentSymbol = grid.GetPoint(new Coordinate(row,column));
                    updatedGrid[row][column] = currentSymbol;

                    if (currentSymbol == DisplaySymbol.Dot)
                    {
                        dotsRemaining++;
                    }
                }
            }

            if (updatedGrid[coordinate.GetRow()][coordinate.GetColumn()] == DisplaySymbol.Dot)
            {
                dotsRemaining--;
            }
            
            updatedGrid[coordinate.GetRow()][coordinate.GetColumn()] = symbol;
            
            return new Grid(updatedGrid, dotsRemaining);
        }
    }
}