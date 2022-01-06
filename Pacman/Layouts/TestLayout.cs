using System.Collections.Generic;

namespace Pacman
{
    public class TestLayout : ILayout
    {
        private int _gridWidth;
        private int _gridHeight;
        private Coordinate _pacmanStartingPosition;
        private List<Coordinate> _wallCoordinates;
        private List<Coordinate> _blankSpacesCoordinates;
        private int _numberOfMonsters;
        private List<Character> _monsterList;

        public TestLayout(Coordinate pacmanStartingPosition, List<Coordinate> monsterCoordinates, List<Coordinate> wallCoordinates, List<Coordinate> blankSpaces, int width = 3, int height = 3, int numberOfMonsters = 0)
        {
            _numberOfMonsters = numberOfMonsters;
            _pacmanStartingPosition = pacmanStartingPosition;
            _monsterList = CreateMonsterList(monsterCoordinates);
            _wallCoordinates = wallCoordinates;
            _blankSpacesCoordinates = blankSpaces;
            _gridWidth = width;
            _gridHeight = height;
        }
        
        public int GetStartingNumberOfDots()
        {
            int startingNumberOfDots = _gridHeight * _gridWidth  - _wallCoordinates.Count - _blankSpacesCoordinates.Count - 1;
            return startingNumberOfDots;
        }
        
        public int GetGridWidth()
        {
            return _gridWidth;
        }
        
        public int GetGridHeight()
        {
            return _gridHeight;
        }

        public List<Coordinate> GetWallCoordinates()
        {
            return _wallCoordinates;
        }
        
        public List<Coordinate> GetBlankSpacesCoordinates()
        {
            return _blankSpacesCoordinates;
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