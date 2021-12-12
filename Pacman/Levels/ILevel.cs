using System.Collections.Generic;

namespace Pacman
{
    public interface ILevel
    {
        public int GetGridWidth();
        public int GetGridHeight();
        public List<Coordinates> GetWallCoordinates();
        public List<Coordinates> GetBlankSpacesCoordinates();
    }
}