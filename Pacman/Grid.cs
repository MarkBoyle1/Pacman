
namespace Pacman
{
    public class Grid
    {
        public string[][] Surface  { get; set; }
        public int DotsRemaining  { get; set; }

        public Grid(string[][] surface, int dotsRemaining)
        {
            Surface = surface;
            DotsRemaining = dotsRemaining;
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
            return DotsRemaining;
        }
        
    }
}