namespace Pacman
{
    public class Engine
    {
        private GridBuilder _gridBuilder = new GridBuilder();
        public Grid PlacePacmanOnStartingPosition(Grid grid, ILevel level)
        {
            return _gridBuilder.UpdateGrid(grid, DisplaySymbol.DefaultPacmanStartingSymbol, level.GetPacmanStartingPosition());
        }
    }
}