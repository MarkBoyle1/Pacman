using System.Collections.Generic;
using Pacman;
using Pacman.Input;
using Pacman.Output;
using Xunit;

namespace PacmanTests
{
    public class CharacterTests
    {
        private GridBuilder _gridBuilder = new GridBuilder();
        
        [Fact]
        public void given_CoordinateEqualsOneOne_and_WallsOnEachSideExceptOneTwo_when_GetPossibleMoves_then_return_ListWithOneTwo()
        {
            ILayout layout = new TestLayout
            (
                new Coordinate(1, 1), new List<Coordinate>(),
                new List<Coordinate>()
                {
                    new Coordinate(1, 0), 
                    new Coordinate(2, 1), 
                    new Coordinate(0, 1)
                },
                new List<Coordinate>());
            
            Grid grid = _gridBuilder.GenerateInitialGrid(layout);

            Character pacman = new PacmanCharacter(new UserInput(), new ConsoleOutput(), layout.GetPacmanStartingPosition());

            List<Coordinate> possibleMoves = pacman.GetPossibleMoves(pacman.Coordinate, grid);
            
            Assert.Single(possibleMoves);
            Assert.Equal(1, possibleMoves[0].GetRow());
            Assert.Equal(2, possibleMoves[0].GetColumn());
        }
        
        [Fact]
        public void given_CoordinateEqualsOneOne_and_WallsOnTwoSides_when_GetPossibleMoves_then_return_ListWithTwoCoordinates()
        {
            ILayout layout = new TestLayout
            (
                new Coordinate(1, 1),
                new List<Coordinate>(),
                new List<Coordinate>()
                {
                    new Coordinate(1, 0), 
                    new Coordinate(2, 1)
                }, 
                new List<Coordinate>());

            Grid grid = _gridBuilder.GenerateInitialGrid(layout);

            Character pacman = new PacmanCharacter(new UserInput(), new ConsoleOutput(), layout.GetPacmanStartingPosition());

            List<Coordinate> possibleMoves = pacman.GetPossibleMoves(pacman.Coordinate, grid);
            
            Assert.Equal(2, possibleMoves.Count);
        }
        
        [Fact]
        public void given_CoordinateEqualsZeroOne_and_NoWalls_when_GetPossibleMoves_then_return_ListWithFourCoordinates()
        {
            Grid grid = _gridBuilder.GenerateEmptyGrid(3, 3);

            Character pacman = new PacmanCharacter(new UserInput(), new ConsoleOutput(), new Coordinate(1, 1));

            List<Coordinate> possibleMoves = pacman.GetPossibleMoves(pacman.Coordinate, grid);
            
            Assert.Equal(4, possibleMoves.Count);
        }
    }
}