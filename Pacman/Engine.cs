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
        private int _currentLevel;
        private ILevel _level;

        public Engine()
        {
            _gridBuilder = new GridBuilder();
            _output = new ConsoleOutput();
            _input = new UserInput();
            _gameScore = 0;
            _currentLevel = 1;
            _characterList = new List<Character>();
        }

        public void RunProgram()
        {
            _level = new TestLevel();
            Character pacman = new PacmanCharacter(_input, _output, _level.GetPacmanStartingPosition());

            _characterList.Add(pacman);
            
            Grid grid = _gridBuilder.GenerateInitialGrid(_level.GetGridWidth(), _level.GetGridHeight(),
                _level.GetWallCoordinates(), _level.GetBlankSpacesCoordinates());
            

            grid = PlacePacmanOnStartingPosition(grid, pacman.Coordinate);
            _gameState = new GameState(grid, _gameScore, _currentLevel, _characterList);
            _output.DisplayGrid(_gameState);

            
            while(true)
            {
                _gameState = PlayOneLevel(_gameState, _level);
                _output.DisplayGrid(_gameState);
            }
        }
        public Grid PlacePacmanOnStartingPosition(Grid grid, Coordinate startingPosition)
        {
            return _gridBuilder.UpdateGrid(grid, DisplaySymbol.DefaultPacmanStartingSymbol, startingPosition);
        }

        public GameState PlayOneLevel(GameState gameState, ILevel level)
        {
            Grid grid = gameState.GetGrid();
            
            while (grid.GetDotsRemaining() > 0)
            {
                gameState = PlayOneTick(gameState);
                grid = gameState.GetGrid();
                _output.DisplayGrid(_gameState);
            }
            
            if (grid.GetDotsRemaining() == 0)
            {
                _currentLevel++;
                grid = _gridBuilder.GenerateInitialGrid(level.GetGridWidth(), level.GetGridHeight(),
                    level.GetWallCoordinates(), level.GetBlankSpacesCoordinates());
                grid = PlacePacmanOnStartingPosition(grid, level.GetPacmanStartingPosition());
                _characterList[0].Coordinate = level.GetPacmanStartingPosition();
            }
            
            return new GameState(grid, _gameScore, _currentLevel, _characterList);
        }

        public GameState PlayOneTick(GameState gameState)
        {
            _characterList = gameState.GetCharacterList();
            Grid grid = gameState.GetGrid();
            Character pacman = gameState.GetCharacterList().First();
            
            grid = MakeCharacterMove(grid, pacman);
            
            return new GameState(grid, _gameScore, _currentLevel, _characterList);
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
            _gameState = new GameState(grid, _gameScore, _currentLevel, _characterList);
            _output.DisplayGrid(_gameState);
                
            grid = _gridBuilder.UpdateGrid(grid, eatingSymbol, coordinate);
            _gameState = new GameState(grid, _gameScore, _currentLevel, _characterList);
            _output.DisplayGrid(_gameState);
        }
    }
}