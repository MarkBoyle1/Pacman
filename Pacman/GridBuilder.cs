using System.Linq;

namespace Pacman
{
    public class GridBuilder
    {
        public Grid GenerateEmptyGrid(int sizeOfGrid)
        {
            string[][] emptyGrid = new string[sizeOfGrid][];

            emptyGrid = emptyGrid.Select(x => new string[sizeOfGrid].Select(x => ".").ToArray()).ToArray();
            
            return new Grid(emptyGrid);
        }
    }
}