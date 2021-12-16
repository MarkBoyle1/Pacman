namespace Pacman
{
    public class Monster : Character
    {
        public Monster(Coordinate coordinate)
        {
            Coordinate = coordinate;
            Symbol = DisplaySymbol.Monster;
        }
    }
}