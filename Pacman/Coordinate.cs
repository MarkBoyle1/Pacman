namespace Pacman
{
    public class Coordinate
    {
        private int Row;
        private int Column;

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