namespace Pacman
{
    public class Grid
    {
        public string[][] Surface;

        public Grid(string[][] surface)
        {
            Surface = surface;
        }

        public string GetPoint(Coordinate coordinate)
        {
            return Surface[coordinate.GetRow()][coordinate.GetColumn()];
        }

        public int GetWidth()
        {
            return 19;
        }

        public int GetHeight()
        {
            return 21;
        }
    }
}