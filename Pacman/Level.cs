using System;
using System.Collections.Generic;

namespace Pacman
{
    public class Level
    {
        private int _levelNumber;
        private int _numberOfMonsters;
        private ILayout _layout;
        private Random _random = new Random();
        private Coordinate _pacmanStartingLocation;
        private List<Character> _monsterList;

        public Level(ILayout layout)
        {
            _layout = layout;
            _numberOfMonsters = layout.GetStartingNumberOfMonsters();
            _levelNumber = 1;
            _pacmanStartingLocation = _layout.GetPacmanStartingPosition();
            _monsterList = CreateMonsters();
        }
        
        public List<Character> GetMonsters()
        {
            return _monsterList;
        }

        public Coordinate GetPacmanStartingPosition()
        {
            return _pacmanStartingLocation;
        }

        public ILayout GetLayout()
        {
            return _layout;
        }

        private List<Character> CreateMonsters()
        {
            List<Character> monsterList = new List<Character>();
        
            for (int i = 0; i < _numberOfMonsters; i++)
            {
                Coordinate coordinate = new Coordinate(0,0);
                bool coordinateIsFreeSpace = false;
        
                while (!coordinateIsFreeSpace)
                {
                    coordinate = GetRandomCoordinate();
                    coordinateIsFreeSpace = CoordinateIsFreeSpace(coordinate);
                }
        
                bool monsterIsOnADot = IsMonsterOnADot(coordinate);
        
                Character newMonster = new Monster(coordinate, monsterIsOnADot);
                
                monsterList.Add(newMonster);
            }
        
            return monsterList;
        }

        private bool IsMonsterOnADot(Coordinate monsterPosition)
        {
            foreach (var coordinate in _layout.GetBlankSpacesCoordinates())
            {
                if (monsterPosition.GetRow() == coordinate.GetRow() &&
                    monsterPosition.GetColumn() == coordinate.GetColumn())
                {
                    return false;
                }
            }
        
            return true;
        }
        
        private Coordinate GetRandomCoordinate()
        {
            int randomRow = _random.Next(0, _layout.GetGridHeight());
            int randomColumn = _random.Next(0, _layout.GetGridWidth());
        
            return new Coordinate(randomRow, randomColumn);
        }
        
        private bool CoordinateIsFreeSpace(Coordinate coordinate)
        {
            foreach (var wallCoordinate in _layout.GetWallCoordinates())
            {
                if (wallCoordinate.GetRow() == coordinate.GetRow() &&
                    wallCoordinate.GetColumn() == coordinate.GetColumn())
                {
                    return false;
                }
            }
        
            return coordinate.GetRow() != _layout.GetPacmanStartingPosition().GetRow() || coordinate.GetColumn() != _layout.GetPacmanStartingPosition().GetColumn();
        }
    }
}