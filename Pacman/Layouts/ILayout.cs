using System.Collections.Generic;

namespace Pacman
{
    public interface ILayout
    {
        public int GetGridWidth();
        public int GetGridHeight();
        public List<Coordinate> GetWallCoordinates();
        public List<Coordinate> GetBlankSpacesCoordinates();
        public Coordinate GetPacmanStartingPosition();
        public int GetStartingNumberOfMonsters();
        public int GetStartingNumberOfDots();
    }
}