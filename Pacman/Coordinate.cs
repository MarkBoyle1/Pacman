namespace Pacman
{
    public class Coordinate
    {
        public int Row  { get;}
        public int Column  { get;}

        public Coordinate(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int GetRow()
        {
            return Row;
        }

        public int GetColumn()
        {
            return Column;
        }
    }
}