namespace Pacman
{
    public class Engine
    {
        private GridBuilder _gridBuilder = new GridBuilder();
        public Grid PlacePacmanOnStartingPosition(Grid grid, ILevel level)
        {
            return _gridBuilder.UpdateGrid(grid, DisplaySymbol.DefaultPacmanStartingSymbol, level.GetPacmanStartingPosition());
        }

        public Grid MakeCharacterMove(Grid grid, Character character)
        {
            grid = _gridBuilder.UpdateGrid(grid, DisplaySymbol.BlankSpace, character.GetCoordinate());
            Coordinate coordinate = character.GetMove();
            return _gridBuilder.UpdateGrid(grid, character.Symbol, coordinate);
        }
    }
}