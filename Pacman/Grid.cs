
namespace Pacman
{
    public class Grid
    {
        public string[][] Surface;
        private int _dotsRemaining;

        public Grid(string[][] surface, int dotsRemaining)
        {
            Surface = surface;
            _dotsRemaining = dotsRemaining;
        }

        public string GetPoint(Coordinate coordinate)
        {
            return Surface[coordinate.GetRow()][coordinate.GetColumn()];
        }

        public int GetWidth()
        {
            return Surface[0].Length;
        }

        public int GetHeight()
        {
            return Surface.Length;
        }

        public int GetDotsRemaining()
        {
            return _dotsRemaining;
        }
        
    }
}