using System;
using System.Collections.Generic;

namespace Pacman
{
    public class Monster : Character
    {
        private Random _random = new Random();
        public bool _currentlyOnADot  { get; set; }
        public Monster(Coordinate coordinate, bool isOnDot)
        {
            Coordinate = coordinate;
            Symbol = DisplaySymbol.Monster;
            _currentlyOnADot = isOnDot;
        }

        public override Coordinate GetMove(Grid grid)
        {
            List<Coordinate> possibleMoves = GetPossibleMoves(Coordinate, grid);
            
            for (int i = possibleMoves.Count - 1; i >= 0; i--)
            {
                if (grid.GetPoint(possibleMoves[i]) == DisplaySymbol.Monster)
                {
                    possibleMoves.RemoveAt(i);
                }
            }

            if (possibleMoves.Count == 0)
            {
                return Coordinate;
            }

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