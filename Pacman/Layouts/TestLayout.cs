using System.Collections.Generic;

namespace Pacman
{
    public class TestLayout : ILayout
    {
        private const int GridWidth = 2;
        private const int GridHeight = 1;
        private Coordinate _pacmanStartingPosition;
        private List<Coordinate> wallCoordinates = new List<Coordinate>();
        private List<Coordinate> blankSpacesCoordinates = new List<Coordinate>();
        private int _numberOfMonsters;
        private List<Character> _monsterList;

        public TestLayout(int numberOfMonsters, Coordinate pacmanStartingPosition, List<Coordinate> monsterCoordinates)
        {
            _numberOfMonsters = numberOfMonsters;
            _pacmanStartingPosition = pacmanStartingPosition;
            _monsterList = CreateMonsterList(monsterCoordinates);
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
            return _pacmanStartingPosition;
        }

        public int GetStartingNumberOfMonsters()
        {
            return _numberOfMonsters;
        }

        public List<Character> GetMonsters()
        {
            return _monsterList;
        }

        private List<Character> CreateMonsterList(List<Coordinate> monsterCoordinates)
        {
            List<Character> monsterList = new List<Character>();

            for (int i = 0; i < monsterCoordinates.Count; i++)
            {
                Coordinate coordinate = monsterCoordinates[i];

                Character newMonster = new Monster(coordinate, false);
                
                monsterList.Add(newMonster);
            }

            return monsterList;
        }
    }
}