using System.Collections.Generic;

namespace Pacman
{
    public class TestLevel : ILevel
    {
        private const int GridWidth = 2;
        private const int GridHeight = 1;
        private Coordinate PacmanStartingPosition = new Coordinate(0, 0);
        private List<Coordinate> wallCoordinates = new List<Coordinate>();
        private List<Coordinate> blankSpacesCoordinates = new List<Coordinate>();

        
        public int GetGridWidth()
        {
            return GridWidth;
        }
        
        public int GetGridHeight()
        {
            return GridHeight;
        }

        public List<Coordinate> GetWallCoordinates()
        {
            return wallCoordinates;
        }
        
        public List<Coordinate> GetBlankSpacesCoordinates()
        {
            return blankSpacesCoordinates;
        }

        public Coordinate GetPacmanStartingPosition()
        {
            return PacmanStartingPosition;
        }
    }
}