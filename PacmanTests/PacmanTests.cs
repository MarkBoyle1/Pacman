using Pacman;
using Xunit;

namespace PacmanTests
{
    public class PacmanTests
    {
        private GridBuilder _gridBuilder = new GridBuilder();
        private Engine _engine = new Engine();
        
        [Fact]
        public void given_StartingLocationEqualsNineEleven_when_PlacePacmanOnStartingLocation_then_NineElevenEqualsPacman()
        {
            ILevel level = new OriginalLayoutLevel();
            
            Grid grid = _gridBuilder.GenerateEmptyGrid(level.GetGridWidth(), level.GetGridHeight());

            grid = _engine.PlacePacmanOnStartingPosition(grid, level);
            
            Assert.Equal(DisplaySymbol.DefaultPacmanStartingSymbol, grid.GetPoint(level.GetPacmanStartingPosition()));
        }
    }
}