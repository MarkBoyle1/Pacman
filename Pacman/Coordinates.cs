namespace Pacman
{
    public class Coordinates
    {
        private int Row;
        private int Column;

        public Coordinates(int row, int column)
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