using System;
using System.Collections.Generic;

namespace Pacman
{
    public class Monster : Character
    {
        private Random _random = new Random();
        private bool _currentlyOnADot;
        public Monster(Coordinate coordinate, bool isOnDot)
        {
            Coordinate = coordinate;
            Symbol = DisplaySymbol.Monster;
            _currentlyOnADot = isOnDot;
        }

        public override Coordinate GetMove(Grid grid)
        {
            List<Coordinate> possibleMoves = GetPossibleMoves(Coordinate, grid);

            int randomIndex = _random.Next(0, possibleMoves.Count);

            Coordinate move = possibleMoves[randomIndex];
            _currentlyOnADot = grid.GetPoint(move) == DisplaySymbol.Dot;
            Coordinate = move;

            return move;
        }

        public override bool IsOnADot()
        {
            return _currentlyOnADot;
        }
    }
}