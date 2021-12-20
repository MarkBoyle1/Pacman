using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Pacman
{
    public class OriginalLayout : ILayout
    {
        private const int GridWidth = 19;
        private const int GridHeight = 21;
        private Coordinate PacmanStartingPosition = new Coordinate(11, 9);
        private int _numberOfMonsters = 4;
        // private List<Character> _monsterList = new List<Character>();
        // private Random _random = new Random();

        private List<Coordinate> wallCoordinates = new List<Coordinate>()
        {
            new Coordinate(0, 0),
            new Coordinate(0, 1),
            new Coordinate(0, 2),
            new Coordinate(0, 3),
            new Coordinate(0, 4),
            new Coordinate(0, 5),
            new Coordinate(0, 6),
            new Coordinate(0, 7),
            new Coordinate(0, 8),
            new Coordinate(0, 9),
            new Coordinate(0, 10),
            new Coordinate(0, 11),
            new Coordinate(0, 12),
            new Coordinate(0, 13),
            new Coordinate(0, 14),
            new Coordinate(0, 15),
            new Coordinate(0, 16),
            new Coordinate(0, 17),
            new Coordinate(0, 18),
            new Coordinate(1, 0),
            new Coordinate(1, 9),
            new Coordinate(1, 18),
            new Coordinate(2, 0),
            new Coordinate(2, 2),
            new Coordinate(2, 3),
            new Coordinate(2, 5),
            new Coordinate(2, 6),
            new Coordinate(2, 7),
            new Coordinate(2, 9),
            new Coordinate(2, 11),
            new Coordinate(2, 12),
            new Coordinate(2, 13),
            new Coordinate(2, 15),
            new Coordinate(2, 16),
            new Coordinate(2, 18),
            new Coordinate(3, 0),
            new Coordinate(3, 18),
            new Coordinate(4, 0),
            new Coordinate(4, 2),
            new Coordinate(4, 3),
            new Coordinate(4, 5),
            new Coordinate(4, 7),
            new Coordinate(4, 8),
            new Coordinate(4, 9),
            new Coordinate(4, 10),
            new Coordinate(4, 11),
            new Coordinate(4, 13),
            new Coordinate(4, 15),
            new Coordinate(4, 16),
            new Coordinate(4, 18),
            new Coordinate(5, 0),
            new Coordinate(5, 5),
            new Coordinate(5, 9),
            new Coordinate(5, 13),
            new Coordinate(5, 18),
            new Coordinate(6, 0),
            new Coordinate(6, 1),
            new Coordinate(6, 2),
            new Coordinate(6, 3),
            new Coordinate(6, 5),
            new Coordinate(6, 6),
            new Coordinate(6, 7),
            new Coordinate(6, 9),
            new Coordinate(6, 11),
            new Coordinate(6, 12),
            new Coordinate(6, 13),
            new Coordinate(6, 15),
            new Coordinate(6, 16),
            new Coordinate(6, 17),
            new Coordinate(6, 18),
            new Coordinate(7, 3),
            new Coordinate(7, 5),
            new Coordinate(7, 13),
            new Coordinate(7, 15),
            new Coordinate(8, 0),
            new Coordinate(8, 1),
            new Coordinate(8, 2),
            new Coordinate(8, 3),
            new Coordinate(8, 5),
            new Coordinate(8, 7),
            new Coordinate(8, 8),
            new Coordinate(8, 10),
            new Coordinate(8, 11),
            new Coordinate(8, 13),
            new Coordinate(8, 15),
            new Coordinate(8, 16),
            new Coordinate(8, 17),
            new Coordinate(8, 18),
            new Coordinate(9, 7),
            new Coordinate(9, 11),
            new Coordinate(10, 0),
            new Coordinate(10, 1),
            new Coordinate(10, 2),
            new Coordinate(10, 3),
            new Coordinate(10, 5),
            new Coordinate(10, 7),
            new Coordinate(10, 8),
            new Coordinate(10, 9),
            new Coordinate(10, 10),
            new Coordinate(10, 11),
            new Coordinate(10, 13),
            new Coordinate(10, 15),
            new Coordinate(10, 16),
            new Coordinate(10, 17),
            new Coordinate(10, 18),
            new Coordinate(11, 3),
            new Coordinate(11, 5),
            new Coordinate(11, 13),
            new Coordinate(11, 15),
            new Coordinate(12, 0),
            new Coordinate(12, 1),
            new Coordinate(12, 2),
            new Coordinate(12, 3),
            new Coordinate(12, 5),
            new Coordinate(12, 7),
            new Coordinate(12, 8),
            new Coordinate(12, 9),
            new Coordinate(12, 10),
            new Coordinate(12, 11),
            new Coordinate(12, 13),
            new Coordinate(12, 15),
            new Coordinate(12, 16),
            new Coordinate(12, 17),
            new Coordinate(12, 18),
            new Coordinate(13, 0),
            new Coordinate(13, 9),
            new Coordinate(13, 18),
            new Coordinate(14, 0),
            new Coordinate(14, 2),
            new Coordinate(14, 3),
            new Coordinate(14, 5),
            new Coordinate(14, 6),
            new Coordinate(14, 7),
            new Coordinate(14, 9),
            new Coordinate(14, 11),
            new Coordinate(14, 12),
            new Coordinate(14, 13),
            new Coordinate(14, 15),
            new Coordinate(14, 16),
            new Coordinate(14, 18),
            new Coordinate(15, 0),
            new Coordinate(15, 3),
            new Coordinate(15, 15),
            new Coordinate(15, 18),
            new Coordinate(16, 0),
            new Coordinate(16, 18),
            new Coordinate(16, 0),
            new Coordinate(16, 1),
            new Coordinate(16, 3),
            new Coordinate(16, 5),
            new Coordinate(16, 7),
            new Coordinate(16, 8),
            new Coordinate(16, 9),
            new Coordinate(16, 10),
            new Coordinate(16, 11),
            new Coordinate(16, 13),
            new Coordinate(16, 15),
            new Coordinate(16, 17),
            new Coordinate(16, 18),
            new Coordinate(17, 0),
            new Coordinate(17, 5),
            new Coordinate(17, 9),
            new Coordinate(17, 13),
            new Coordinate(17, 18),
            new Coordinate(18, 0),
            new Coordinate(18, 2),
            new Coordinate(18, 3),
            new Coordinate(18, 4),
            new Coordinate(18, 5),
            new Coordinate(18, 6),
            new Coordinate(18, 7),
            new Coordinate(18, 9),
            new Coordinate(18, 11),
            new Coordinate(18, 12),
            new Coordinate(18, 13),
            new Coordinate(18, 14),
            new Coordinate(18, 15),
            new Coordinate(18, 16),
            new Coordinate(18, 18),
            new Coordinate(19, 0),
            new Coordinate(19, 18),
            new Coordinate(20, 0),
            new Coordinate(20, 1),
            new Coordinate(20, 2),
            new Coordinate(20, 3),
            new Coordinate(20, 4),
            new Coordinate(20, 5),
            new Coordinate(20, 6),
            new Coordinate(20, 7),
            new Coordinate(20, 8),
            new Coordinate(20, 9),
            new Coordinate(20, 10),
            new Coordinate(20, 11),
            new Coordinate(20, 12),
            new Coordinate(20, 13),
            new Coordinate(20, 14),
            new Coordinate(20, 15),
            new Coordinate(20, 16),
            new Coordinate(20, 17),
            new Coordinate(20, 18)
        };

        public List<Coordinate> blankSpacesCoordinates = new List<Coordinate>()
        {
            new Coordinate(7, 0),
            new Coordinate(7, 1),
            new Coordinate(7, 2),
            new Coordinate(7, 16),
            new Coordinate(7, 17),
            new Coordinate(7, 18),
            new Coordinate(8,9),
            new Coordinate(9, 8),
            new Coordinate(9, 9),
            new Coordinate(9, 10),
            new Coordinate(11, 0),
            new Coordinate(11, 1),
            new Coordinate(11, 2),
            new Coordinate(11, 16),
            new Coordinate(11, 17),
            new Coordinate(11, 18)
        };

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
        
        public int GetStartingNumberOfMonsters()
        {
            return _numberOfMonsters;
        }

        // public List<Character> GetMonsters()
        // {
        //     return CreateMonsters();
        // }

        // private List<Character> CreateMonsters()
        // {
        //     List<Character> monsterList = new List<Character>();
        //
        //     for (int i = 0; i < _numberOfMonsters; i++)
        //     {
        //         Coordinate coordinate = new Coordinate(0,0);
        //         bool coordinateIsFreeSpace = false;
        //
        //         while (!coordinateIsFreeSpace)
        //         {
        //             coordinate = GetRandomCoordinate();
        //             coordinateIsFreeSpace = CoordinateIsFreeSpace(coordinate);
        //         }
        //
        //         bool monsterIsOnADot = IsMonsterOnADot(coordinate);
        //
        //         Character newMonster = new Monster(coordinate, monsterIsOnADot);
        //         
        //         monsterList.Add(newMonster);
        //     }
        //
        //     return monsterList;
        // }

        // private bool IsMonsterOnADot(Coordinate monsterPosition)
        // {
        //     foreach (var coordinate in blankSpacesCoordinates)
        //     {
        //         if (monsterPosition.GetRow() == coordinate.GetRow() &&
        //             monsterPosition.GetColumn() == coordinate.GetColumn())
        //         {
        //             return false;
        //         }
        //     }
        //
        //     return true;
        // }
        //
        // private Coordinate GetRandomCoordinate()
        // {
        //     int randomRow = _random.Next(0, GridHeight);
        //     int randomColumn = _random.Next(0, GridWidth);
        //
        //     return new Coordinate(randomRow, randomColumn);
        // }
        //
        // private bool CoordinateIsFreeSpace(Coordinate coordinate)
        // {
        //     foreach (var wallCoordinate in wallCoordinates)
        //     {
        //         if (wallCoordinate.GetRow() == coordinate.GetRow() &&
        //             wallCoordinate.GetColumn() == coordinate.GetColumn())
        //         {
        //             return false;
        //         }
        //     }
        //
        //     return coordinate.GetRow() != PacmanStartingPosition.GetRow() && coordinate.GetColumn() != PacmanStartingPosition.GetColumn();
        // }
    }
}