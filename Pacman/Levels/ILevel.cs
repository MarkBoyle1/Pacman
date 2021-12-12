using System.Collections.Generic;

namespace Pacman
{
    public interface ILevel
    {
        public int GetGridWidth();
        public int GetGridHeight();
        public List<Coordinate> GetWallCoordinates();
        public List<Coordinate> GetBlankSpacesCoordinates();
    }
}