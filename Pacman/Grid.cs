namespace Pacman
{
    public class Grid
    {
        public string[][] Surface;

        public Grid(string[][] surface)
        {
            this.Surface = surface;
        }

        public string GetPoint(int row, int column)
        {
            return Surface[row][column];
        }
    }
}