using System.Collections.Generic;
using System.Linq;

namespace Pacman
{
    public class GridBuilder
    {

        public Grid GenerateInitialGrid(int width, int height, List<Coordinates> wallCoordinates,
            List<Coordinates> blankSpacesCoordinates)
        {
            Grid grid = GenerateEmptyGrid(width, height);
            grid = AddWalls(grid, wallCoordinates);
            grid = AddBlankSpaces(grid, blankSpacesCoordinates);

            return grid;
        }
        public Grid GenerateEmptyGrid(int width, int height)
        {
            string[][] emptyGrid = new string[height][];

            emptyGrid = emptyGrid.Select(x => new string[width].Select(x => DisplaySymbol.Dot).ToArray()).ToArray();
            
            return new Grid(emptyGrid);
        }

        public Grid AddWalls(Grid grid, List<Coordinates> coordinates)
        {
            foreach (var coordinate in coordinates)
            {
                grid.Surface[coordinate.GetRow()][coordinate.GetColumn()] = DisplaySymbol.Wall;
            }
            
            return grid;
        }
        
        public Grid AddBlankSpaces(Grid grid, List<Coordinates> coordinates)
        {
            foreach (var coordinate in coordinates)
            {
                grid.Surface[coordinate.GetRow()][coordinate.GetColumn()] = DisplaySymbol.BlankSpace;
            }
            
            return grid;
        }
    }
}