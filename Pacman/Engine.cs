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
        private int _livesLeft;
        private List<Character> _characterList;
        private int _currentLevel;
        private Level _level;
        private ILayout _layout;

        public Engine(ILayout layout)
        {
            _gridBuilder = new GridBuilder();
            _output = new ConsoleOutput();
            _input = new UserInput();
            _gameScore = 0;
            _currentLevel = 1;
            _livesLeft = 3;
            _characterList = new List<Character>();
            _layout = layout;
            _level = new Level(1, _layout);
        }

        public void RunProgram()
        {
            Character pacman = new PacmanCharacter(_input, _output, _level.GetPacmanStartingPosition());

            _characterList = CreateCharacterList(pacman, _level);
            
            Grid grid = _gridBuilder.GenerateInitialGrid(_level.GetLayout());
            
            grid = PlaceCharactersOnGrid(grid, _characterList);
            
            GameState gameState = new GameState(grid, _gameScore, _currentLevel, _livesLeft, _characterList);
            
            _output.DisplayGrid(gameState);
            
            while(gameState.GetLivesLeft() > 0)
            {
                gameState = PlayOneLevel(gameState, _level);
                _output.DisplayGrid(gameState);
            }
        }

        public List<Character> CreateCharacterList(Character pacman, Level level)
        {
            List<Character> characters = new List<Character>();
            characters.Add(pacman);

            List<Character> monsters = level.GetMonsters();

            foreach (var monster in monsters)
            {
                characters.Add(monster);
            }

            return characters;
        }

        public Grid PlaceCharactersOnGrid(Grid grid, List<Character> characters)
        {
            foreach (var character in characters)
            {
                grid = _gridBuilder.UpdateGrid(grid, character.Symbol, character.Coordinate);
            }

            return grid;
        }

        public GameState PlayOneLevel(GameState gameState, Level level)
        {
            Grid grid = gameState.GetGrid();
            
            while (grid.GetDotsRemaining() > 0 && gameState.GetLivesLeft() > 0)
            {
                gameState = PlayOneTick(gameState);
                grid = gameState.GetGrid();
                _output.DisplayGrid(gameState);
            }
            
            if (grid.GetDotsRemaining() == 0)
            {
                _currentLevel++;
                _level = new Level(_currentLevel, _layout);

                _characterList = CreateCharacterList(gameState.GetCharacterList().First(), _level);

                gameState.GetCharacterList().First().Coordinate = level.GetPacmanStartingPosition();
                gameState.GetCharacterList().First().Symbol = DisplaySymbol.DefaultPacmanStartingSymbol;
                
                grid = _gridBuilder.GenerateInitialGrid(level.GetLayout());
                
                grid = PlaceCharactersOnGrid(grid, _characterList);
            }
            
            return new GameState(grid, gameState.GetScore(), _currentLevel, gameState.GetLivesLeft(), _characterList);
        }

        public GameState PlayOneTick(GameState gameState)
        {
            foreach (var character in gameState.GetCharacterList())
            {
                gameState = MakeCharacterMove(gameState, character);
                bool pacmanIsDead = CheckForPacmanDeath(gameState);

                if (pacmanIsDead)
                {
                    _output.DisplayDeathAnimation(gameState);
                    gameState = UpdateGameStateForPacmanDeath(gameState, _level);
                    return gameState;
                }
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

            gameState = new GameState(grid, gameState.GetScore(), gameState.GetLevel(), gameState.GetLivesLeft(), gameState.GetCharacterList());
            Coordinate coordinate = character.GetMove(grid);

            if (grid.GetPoint(coordinate) == DisplaySymbol.Dot && character.GetType() == typeof(PacmanCharacter))
            {
                int gameScore = gameState.GetScore() + 1;
                gameState = new GameState(grid, gameScore, gameState.GetLevel(),  gameState.GetLivesLeft(), gameState.GetCharacterList());

                gameState = MakeEatingAnimation(gameState, character, coordinate);
                return gameState;
            }
            
            grid = _gridBuilder.UpdateGrid(grid, character.Symbol, coordinate);
            return new GameState(grid, gameState.GetScore(), gameState.GetLevel(), gameState.GetLivesLeft(), gameState.GetCharacterList());
        }

        private GameState MakeEatingAnimation(GameState gameState, Character character, Coordinate coordinate)
        {
            Grid grid = gameState.GetGrid();
            string eatingSymbol =
                character.Symbol is DisplaySymbol.PacmanEastFacing or DisplaySymbol.PacmanWestFacing
                    ? DisplaySymbol.PacmanHorizontalEating
                    : DisplaySymbol.PacmanVerticalEating;
                
            grid = _gridBuilder.UpdateGrid(grid, character.Symbol, coordinate);
            gameState = new GameState(grid, gameState.GetScore(), gameState.GetLevel(), gameState.GetLivesLeft(), gameState.GetCharacterList());
            _output.DisplayGrid(gameState);
                
            grid = _gridBuilder.UpdateGrid(grid, eatingSymbol, coordinate);
            gameState = new GameState(grid, gameState.GetScore(), gameState.GetLevel(), gameState.GetLivesLeft(), gameState.GetCharacterList());
            _output.DisplayGrid(gameState);
            
            grid = _gridBuilder.UpdateGrid(grid, character.Symbol, coordinate);
            gameState = new GameState(grid, gameState.GetScore(), gameState.GetLevel(), gameState.GetLivesLeft(), gameState.GetCharacterList());
            _output.DisplayGrid(gameState);

            return gameState;
        }

        public GameState UpdateGameStateForPacmanDeath(GameState gameState, Level level)
        {
            int livesLeft = gameState.GetLivesLeft() - 1;
            gameState.GetCharacterList().First().Coordinate = level.GetPacmanStartingPosition();
            Grid grid = _gridBuilder.UpdateGrid(gameState.GetGrid(), DisplaySymbol.DefaultPacmanStartingSymbol, level.GetPacmanStartingPosition());

            return new GameState(grid, gameState.GetScore(), gameState.GetLevel(),
                livesLeft, gameState.GetCharacterList());
        }

        private bool CheckForPacmanDeath(GameState gameState)
        {
            List<Character> characterList = gameState.GetCharacterList();
            Coordinate pacmanLocation = characterList[0].Coordinate;

            foreach (var character in characterList)
            {
                if (character.Coordinate.GetRow() == pacmanLocation.GetRow() &&
                    character.Coordinate.GetColumn() == pacmanLocation.GetColumn() &&
                    character.GetType() == typeof(Monster))
                {
                    return true;
                }
            }

            return false;
        }
    }
}