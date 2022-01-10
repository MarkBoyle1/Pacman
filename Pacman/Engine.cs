using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Pacman.Exceptions;
using Pacman.Input;
using Pacman.Output;

namespace Pacman
{
    public class Engine
    {
        private GridBuilder _gridBuilder;
        private IOutput _output;
        private int _highScore;
        private List<Character> _characterList;
        private int _currentLevel = 1;
        private Level _level;
        private ILayout _layout;
        private int _numberOfDotsRemaining;
        private GameSetUp _gameSetUp;

        public Engine(ILayout layout, IUserInput input, IOutput output, string highScoreFilePath = Constants.HighScoreFilePath, string savedGameFilePath = Constants.SavedGameFilePath)
        {
            _highScore = Convert.ToInt16(File.ReadAllText(highScoreFilePath));
            _gridBuilder = new GridBuilder();
            _output = output;
            _characterList = new List<Character>();
            _layout = layout;
            _level = new Level(1, _layout);
            _numberOfDotsRemaining = _layout.GetStartingNumberOfDots();
            _gameSetUp = new GameSetUp(output, input, _level);
        }

        public void RunProgram()
        {
            _output.DisplayMessage(OutputMessages.Welcome);
            GameState gameState = _gameSetUp.GetInitialGameState();

            _numberOfDotsRemaining = gameState.Grid.DotsRemaining;
            _characterList = gameState.CharacterList;
            _currentLevel = gameState.LevelNumber;

            _output.SetHighScore(_highScore);
            _output.DisplayGameState(gameState);
            
            while(gameState.LivesLeft > 0)
            {
                gameState = PlayOneLevel(gameState, _level);
                _output.DisplayGameState(gameState);
            }

            _highScore = UpdateHighScoreIfRequired(gameState.Score, _highScore);
            File.WriteAllTextAsync(Constants.HighScoreFilePath, _highScore.ToString());
        }
        
        public GameState PlayOneLevel(GameState gameState, Level level)
        {
            Grid grid = gameState.Grid;
            
            while (_numberOfDotsRemaining > 0 && gameState.LivesLeft > 0)
            {
                gameState = PlayOneTick(gameState);
                grid = gameState.Grid;
                _output.DisplayGameState(gameState);
            }
            
            if (_numberOfDotsRemaining == 0)
            {
                _currentLevel++;
                _level = new Level(_currentLevel, _layout);
                _numberOfDotsRemaining = _layout.GetStartingNumberOfDots();

                _characterList = _gameSetUp.CreateCharacterList(_level);

                gameState.GetPacman().Coordinate = level.GetPacmanStartingPosition();
                gameState.GetPacman().Symbol = DisplaySymbol.DefaultPacmanStartingSymbol;
                
                grid = _gridBuilder.GenerateInitialGrid(level.GetLayout());
                
                grid = _gameSetUp.PlaceCharactersOnGrid(grid, _characterList);
            }
            
            return new GameState(grid, gameState.Score, _currentLevel, gameState.LivesLeft, _characterList);
        }

        public GameState PlayOneTick(GameState gameState)
        {
            foreach (var character in gameState.CharacterList)
            {
                gameState = MoveCharacter(gameState, character);
                
                bool pacmanIsDead = CheckForPacmanDeath(gameState);

                if (pacmanIsDead)
                {
                    _output.DisplayDeathAnimation(gameState);
                    gameState = UpdateGameStateForPacmanDeath(gameState, _level);
                    return gameState;
                }
            }
            _output.DisplayGameState(gameState);
            return gameState;
        }

        public GameState MoveCharacter(GameState gameState, Character character)
        {
            Grid grid = gameState.Grid;

            //Updating the space of the old location for the character
            if (character.GetType() == typeof(Monster))
            {
                string gridSymbol = character.IsOnADot ? DisplaySymbol.Dot : DisplaySymbol.BlankSpace;
                grid = _gridBuilder.UpdateGrid(grid, gridSymbol, character.GetCoordinate());
            }
            else
            {
                grid = _gridBuilder.UpdateGrid(grid, DisplaySymbol.BlankSpace, character.GetCoordinate());
            }
            
            Coordinate coordinate;
            
            try
            {
                coordinate = character.GetMove(grid);
            }
            catch (InputIsSaveException)
            {
                SaveGame(gameState);
                _output.DisplayMessageWithDelay(OutputMessages.GameSaved);
                return gameState;
            }

            //Pacman eating a dot
            if (grid.GetPoint(coordinate) == DisplaySymbol.Dot && character.GetType() == typeof(PacmanCharacter))
            {
                _numberOfDotsRemaining--;
                int gameScore = gameState.Score + 1;
                gameState = new GameState(grid, gameScore, gameState.LevelNumber,  gameState.LivesLeft, gameState.CharacterList);

                gameState = MakeEatingAnimation(gameState, character, coordinate);
                return gameState;
            }
            
            grid = _gridBuilder.UpdateGrid(grid, character.Symbol, coordinate);

            return new GameState(grid, gameState.Score, gameState.LevelNumber, gameState.LivesLeft, gameState.CharacterList);
        }

        private GameState MakeEatingAnimation(GameState gameState, Character character, Coordinate coordinate)
        {
            Grid grid = gameState.Grid;
            string eatingSymbol =
                character.Symbol is DisplaySymbol.PacmanEastFacing or DisplaySymbol.PacmanWestFacing
                    ? DisplaySymbol.PacmanHorizontalEating
                    : DisplaySymbol.PacmanVerticalEating;

            grid = _gridBuilder.UpdateGrid(grid, character.Symbol, coordinate);
            Grid gridWithMouthClosed = _gridBuilder.UpdateGrid(grid, eatingSymbol, coordinate);
            gameState = new GameState(grid, gameState.Score, gameState.LevelNumber, gameState.LivesLeft, gameState.CharacterList);

            _output.DisplayEatingAnimation(gameState, grid, gridWithMouthClosed);
            
            return gameState;
        }

        public GameState UpdateGameStateForPacmanDeath(GameState gameState, Level level)
        {
            int livesLeft = gameState.LivesLeft - 1;
            
            //Leave the monster in the location where the death occured
            Grid grid = _gridBuilder.UpdateGrid(gameState.Grid, DisplaySymbol.Monster, gameState.GetPacman().Coordinate);
            
            //Put pacman back in starting position
            gameState.GetPacman().Coordinate = level.GetPacmanStartingPosition();
            grid = _gridBuilder.UpdateGrid(grid, DisplaySymbol.DefaultPacmanStartingSymbol, level.GetPacmanStartingPosition());

            return new GameState(grid, gameState.Score, gameState.LevelNumber,
                livesLeft, gameState.CharacterList);
        }

        private bool CheckForPacmanDeath(GameState gameState)
        {
            List<Character> characterList = gameState.CharacterList;
            Coordinate pacmanLocation = gameState.GetPacman().Coordinate;

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

        public int UpdateHighScoreIfRequired(int gameScore, int highScore)
        {
            if (gameScore > highScore)
            {
                highScore = gameScore;
                _output.SetHighScore(highScore);
            }

            return highScore;
        }

        private void SaveGame(GameState gameState)
        {
            string gameStateJsonString = JsonSerializer.Serialize(gameState);
            File.WriteAllTextAsync(Constants.SavedGameFilePath, gameStateJsonString);
        }
    }
}