using System;
using System.Collections.Generic;
using System.Linq;
using Pacman.Input;
using Pacman.Output;

namespace Pacman
{
    public class Engine
    {
        private GridBuilder _gridBuilder;
        private IOutput _output;
        private IUserInput _input;
        private int _gameScore;
        private List<Character> _characterList;
        private GameState _gameState;

        public Engine()
        {
            _gridBuilder = new GridBuilder();
            _output = new ConsoleOutput();
            _input = new UserInput();
            _gameScore = 0;
            _characterList = new List<Character>();
        }

        public void RunProgram()
        {
            ILevel level = new OriginalLayoutLevel();
            Character pacman = new PacmanCharacter(_input, _output, level.GetPacmanStartingPosition());

            _characterList.Add(pacman);
            
            Grid grid = _gridBuilder.GenerateInitialGrid(level.GetGridWidth(), level.GetGridHeight(),
                level.GetWallCoordinates(), level.GetBlankSpacesCoordinates());
            

            grid = PlacePacmanOnStartingPosition(grid, pacman.Coordinate);
            _gameState = new GameState(grid, _gameScore, _characterList);
            _output.DisplayGrid(_gameState);

            
            while(_gameState.GetGrid().GetDotsRemaining() > 0)
            {
                _gameState = PlayOneTick(_gameState);
                _output.DisplayGrid(_gameState);
            }
        }
        public Grid PlacePacmanOnStartingPosition(Grid grid, Coordinate startingPosition)
        {
            return _gridBuilder.UpdateGrid(grid, DisplaySymbol.DefaultPacmanStartingSymbol, startingPosition);
        }

        public GameState PlayOneTick(GameState gameState)
        {
            _characterList = gameState.GetCharacterList();
            Grid grid = gameState.GetGrid();
            Character pacman = gameState.GetCharacterList().First();
            
            grid = MakeCharacterMove(grid, pacman);
            return new GameState(grid, _gameScore, _characterList);
        }

        public Grid MakeCharacterMove(Grid grid, Character character)
        {
            grid = _gridBuilder.UpdateGrid(grid, DisplaySymbol.BlankSpace, character.GetCoordinate());
            Coordinate coordinate = character.GetMove(grid);

            if (grid.GetPoint(coordinate) == DisplaySymbol.Dot)
            {
                _gameScore++;
                MakeEatingAnimation(grid, character, coordinate);
            }
            
            return _gridBuilder.UpdateGrid(grid, character.Symbol, coordinate);
        }

        private void MakeEatingAnimation(Grid grid, Character character, Coordinate coordinate)
        {
            string eatingSymbol =
                character.Symbol is DisplaySymbol.PacmanEastFacing or DisplaySymbol.PacmanWestFacing
                    ? DisplaySymbol.PacmanHorizontalEating
                    : DisplaySymbol.PacmanVerticalEating;
                
            grid = _gridBuilder.UpdateGrid(grid, character.Symbol, coordinate);
            _gameState = new GameState(grid, _gameScore, _characterList);
            _output.DisplayGrid(_gameState);
                
            grid = _gridBuilder.UpdateGrid(grid, eatingSymbol, coordinate);
            _gameState = new GameState(grid, _gameScore, _characterList);
            _output.DisplayGrid(_gameState);
        }
    }
}