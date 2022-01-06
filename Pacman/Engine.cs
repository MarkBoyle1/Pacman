using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Pacman.Exceptions;
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
        private int _highScore;
        private int _livesLeft;
        private List<Character> _characterList;
        private int _currentLevel;
        private Level _level;
        private ILayout _layout;
        private int _numberOfDotsRemaining;
        private string _savedGameFilePath;

        public Engine(ILayout layout, IUserInput input, IOutput output, string highScoreFilePath = Constants.HighScoreFilePath, string savedGameFilePath = Constants.SavedGameFilePath)
        {
            _highScore = Convert.ToInt16(File.ReadAllText(highScoreFilePath));
            _gridBuilder = new GridBuilder();
            _output = output;
            _input = input;
            _gameScore = 0;
            _currentLevel = 1;
            _livesLeft = 3;
            _characterList = new List<Character>();
            _layout = layout;
            _level = new Level(1, _layout);
            _numberOfDotsRemaining = _layout.GetStartingNumberOfDots();
            _savedGameFilePath = savedGameFilePath;
        }

        public void RunProgram()
        {
            bool userWantsToStartNewGame = CheckIfUserWantsToStartNewGame();
            GameState gameState;

            if (userWantsToStartNewGame)
            {
                Character pacman = new PacmanCharacter(_input, _output, _level.GetPacmanStartingPosition());
                
                _characterList = CreateCharacterList(pacman, _level);
                
                Grid grid = _gridBuilder.GenerateInitialGrid(_level.GetLayout());
                
                grid = PlaceCharactersOnGrid(grid, _characterList);
                
                gameState = new GameState(grid, _gameScore, _currentLevel, _livesLeft, _characterList);
            }
            else
            {
                gameState = LoadPreviousGame();
                _characterList = gameState.GetCharacterList();
            }
            

            
            
            _output.SetHighScore(_highScore);
            _output.DisplayGrid(gameState);
            
            while(gameState.GetLivesLeft() > 0)
            {
                gameState = PlayOneLevel(gameState, _level);
                _output.DisplayGrid(gameState);
            }

            _highScore = UpdateHighScoreIfRequired(gameState.GetScore(), _highScore);
            File.WriteAllTextAsync(Constants.HighScoreFilePath, _highScore.ToString());

        }

        private bool CheckIfUserWantsToStartNewGame()
        {
            _output.DisplayMessage(OutputMessages.AskToStartNewGameOrLoadPreviousGame);
            string response = _input.GetUserInput();

            while (true)
            {
                if (response == Constants.ResponseToStartNewGame)
                {
                    return true;
                }
                
                if (response == Constants.ResponseToLoadSavedGame)
                {
                    return false;
                }
                
                _output.DisplayMessage(OutputMessages.InvalidInput);
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
            
            while (_numberOfDotsRemaining > 0 && gameState.GetLivesLeft() > 0)
            {
                gameState = PlayOneTick(gameState);
                grid = gameState.GetGrid();
                _output.DisplayGrid(gameState);
            }
            
            if (_numberOfDotsRemaining == 0)
            {
                _currentLevel++;
                _level = new Level(_currentLevel, _layout);
                _numberOfDotsRemaining = _layout.GetStartingNumberOfDots();

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
            
            Coordinate coordinate;
            
            try
            {
                coordinate = character.GetMove(grid);
            }
            catch (InputIsSaveException)
            {
                SaveGame(gameState);
                return gameState;
            }

            if (grid.GetPoint(coordinate) == DisplaySymbol.Dot && character.GetType() == typeof(PacmanCharacter))
            {
                _numberOfDotsRemaining--;
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
            Grid grid = _gridBuilder.UpdateGrid(gameState.GetGrid(), DisplaySymbol.Monster, gameState.GetCharacterList().First().Coordinate);
            gameState.GetCharacterList().First().Coordinate = level.GetPacmanStartingPosition();
            grid = _gridBuilder.UpdateGrid(grid, DisplaySymbol.DefaultPacmanStartingSymbol, level.GetPacmanStartingPosition());

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
        
        public GameState LoadPreviousGame()
        {
            var myJsonString = File.ReadAllText(_savedGameFilePath);
            var myJObject = JObject.Parse(myJsonString);
            List<JProperty> properties = myJObject.Properties().ToList();
            
            JProperty gridProperty = properties[0];
            JProperty scoreProperty = properties[1];
            JProperty levelProperty = properties[2];
            JProperty livesLeftProperty = properties[3];
            JProperty characterListProperty = properties[4];

            Grid grid = new Grid
                (
                    gridProperty.Value["Surface"].ToObject<string[][]>(), 
                    gridProperty.Value["_dotsRemaining"].ToObject<int>()
                );

            int score = scoreProperty.Value.ToObject<int>();
            int level = levelProperty.Value.ToObject<int>();
            int livesLeft = livesLeftProperty.Value.ToObject<int>();
            
            List<Character> characterList = new List<Character>();

            for (int i = 0; i < characterListProperty.Value.Count(); i++)
            {
                Character character;
                string row = characterListProperty.Value[i]["Coordinate"]["Row"].ToString();
                string column = characterListProperty.Value[i]["Coordinate"]["Column"].ToString();

                if (i == 0)
                {
                    character = new PacmanCharacter(_input, _output, new Coordinate(Convert.ToInt16(row), Convert.ToInt16(column)));
                }
                else
                {
                    character = new Monster(new Coordinate(Convert.ToInt16(row), Convert.ToInt16(column)), true);
                }

                characterList.Add(character);
            }

            return new GameState(grid, score, level, livesLeft, characterList);
        }
    }
}