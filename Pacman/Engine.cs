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
        // private GameState _gameState;
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
            _level = new OriginalLayoutLevel();
            Character pacman = new PacmanCharacter(_input, _output, _level.GetPacmanStartingPosition());

            List<Character> monsterList = _level.GetMonsters();

            _characterList.Add(pacman);

            foreach (var monster in monsterList)
            {
                _characterList.Add(monster);
            }
            
            Grid grid = _gridBuilder.GenerateInitialGrid(_level.GetGridWidth(), _level.GetGridHeight(),
                _level.GetWallCoordinates(), _level.GetBlankSpacesCoordinates());
            
            grid = PlaceCharactersOnGrid(grid, _characterList);
            GameState gameState = new GameState(grid, _gameScore, _currentLevel, _characterList);
            _output.DisplayGrid(gameState);
            
            while(true)
            {
                gameState = PlayOneLevel(gameState, _level);
                _output.DisplayGrid(gameState);
            }
        }
        
        public Grid PlaceCharactersOnGrid(Grid grid, List<Character> characters)
        {
            foreach (var character in characters)
            {
                grid = _gridBuilder.UpdateGrid(grid, character.Symbol, character.Coordinate);
            }

            return grid;
        }

        public GameState PlayOneLevel(GameState gameState, ILevel level)
        {
            Grid grid = gameState.GetGrid();
            
            while (grid.GetDotsRemaining() > 0)
            {
                gameState = PlayOneTick(gameState);
                grid = gameState.GetGrid();
                _output.DisplayGrid(gameState);
            }
            
            if (grid.GetDotsRemaining() == 0)
            {
                _currentLevel++;
                grid = _gridBuilder.GenerateInitialGrid(level.GetGridWidth(), level.GetGridHeight(),
                    level.GetWallCoordinates(), level.GetBlankSpacesCoordinates());
                grid = PlaceCharactersOnGrid(grid, gameState.GetCharacterList());
                gameState.GetCharacterList()[0].Coordinate = level.GetPacmanStartingPosition();
            }
            
            return new GameState(grid, _gameScore, _currentLevel, _characterList);
        }

        public GameState PlayOneTick(GameState gameState)
        {
            foreach (var character in gameState.GetCharacterList())
            {
                gameState = MakeCharacterMove(gameState, character);
                
            }
            _output.DisplayGrid(gameState);
            return gameState;
        }

        public GameState MakeCharacterMove(GameState gameState, Character character)
        {
            Grid grid = gameState.GetGrid();

            if (character.GetType() == typeof(Monster))
            {
                string gridSymbol = character.IsOnADot() ? DisplaySymbol.Dot : DisplaySymbol.BlankSpace;
                grid = _gridBuilder.UpdateGrid(grid, gridSymbol, character.GetCoordinate());
            }
            else
            {
                grid = _gridBuilder.UpdateGrid(grid, DisplaySymbol.BlankSpace, character.GetCoordinate());
            }

            gameState = new GameState(grid, gameState.GetScore(), gameState.GetLevel(), gameState.GetCharacterList());
            Coordinate coordinate = character.GetMove(grid);

            if (grid.GetPoint(coordinate) == DisplaySymbol.Dot && character.GetType() == typeof(PacmanCharacter))
            {
                int gameScore = gameState.GetScore() + 1;
                gameState = new GameState(grid, gameScore, gameState.GetLevel(), gameState.GetCharacterList());

                gameState = MakeEatingAnimation(gameState, character, coordinate);
                return gameState;
            }
            
            grid = _gridBuilder.UpdateGrid(grid, character.Symbol, coordinate);
            return new GameState(grid, gameState.GetScore(), gameState.GetLevel(), gameState.GetCharacterList());
        }

        private GameState MakeEatingAnimation(GameState gameState, Character character, Coordinate coordinate)
        {
            Grid grid = gameState.GetGrid();
            string eatingSymbol =
                character.Symbol is DisplaySymbol.PacmanEastFacing or DisplaySymbol.PacmanWestFacing
                    ? DisplaySymbol.PacmanHorizontalEating
                    : DisplaySymbol.PacmanVerticalEating;
                
            grid = _gridBuilder.UpdateGrid(grid, character.Symbol, coordinate);
            gameState = new GameState(grid, gameState.GetScore(), gameState.GetLevel(), gameState.GetCharacterList());
            _output.DisplayGrid(gameState);
                
            grid = _gridBuilder.UpdateGrid(grid, eatingSymbol, coordinate);
            gameState = new GameState(grid, gameState.GetScore(), gameState.GetLevel(), gameState.GetCharacterList());
            _output.DisplayGrid(gameState);
            
            grid = _gridBuilder.UpdateGrid(grid, character.Symbol, coordinate);
            gameState = new GameState(grid, gameState.GetScore(), gameState.GetLevel(), gameState.GetCharacterList());
            _output.DisplayGrid(gameState);

            return gameState;
        }
    }
}