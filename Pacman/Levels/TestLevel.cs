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
        private int _numberOfMonsters;
        private List<Character> _monsterList;

        public TestLevel(int numberOfMonsters)
        {
            _numberOfMonsters = numberOfMonsters;
            _monsterList = CreateMonsterList();
        }
        
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

        public List<Character> GetMonsters()
        {
            return _monsterList;
        }

        private List<Character> CreateMonsterList()
        {
            List<Character> monsterList = new List<Character>();

            for (int i = 0; i < _numberOfMonsters; i++)
            {
                Coordinate coordinate = new Coordinate(0, 1);

                Character newMonster = new Monster(coordinate);
                
                monsterList.Add(newMonster);
            }

            return monsterList;
        }
    }
}