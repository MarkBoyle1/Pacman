using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using Pacman.Input;
using Pacman.Output;

namespace Pacman
{
    public class GameSetUp
    {
        private IOutput _output;
        private IUserInput _input;
        private GridBuilder _gridBuilder;
        private Level _level;
        private string _savedGameFilePath;

        public GameSetUp(IOutput output, IUserInput input, Level level, string savedGameFilePath = Constants.SavedGameFilePath)
        {
            _output = output;
            _input = input;
            _level = level;
            _savedGameFilePath = savedGameFilePath;
            _gridBuilder = new GridBuilder();
        }

        public GameState GetInitialGameState()
        {
            bool userWantsToStartNewGame = CheckIfUserWantsToStartNewGame();
            GameState gameState;

            if (userWantsToStartNewGame)
            {
                List<Character> characterList = CreateCharacterList(_level);
                
                Grid grid = _gridBuilder.GenerateInitialGrid(_level.GetLayout());
                
                grid = PlaceCharactersOnGrid(grid, characterList);
                
                int gameScore = 0;
                int currentLevel = 1;
                int livesLeft = 3;
                
                gameState = new GameState(grid, gameScore, currentLevel, livesLeft, characterList);
            }
            else
            {
                gameState = LoadPreviousGame();
            }

            return gameState;
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
                response = _input.GetUserInput();
            }
        }
        
        public List<Character> CreateCharacterList(Level level)
        {
            Character pacman = new PacmanCharacter(_input, _output, _level.GetPacmanStartingPosition());

            List<Character> characters = new List<Character> {pacman};

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
                gridProperty.Value["DotsRemaining"].ToObject<int>()
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
                string isOnADot = characterListProperty.Value[i]["IsOnADot"].ToString();

                if (i == 0)
                {
                    character = new PacmanCharacter(_input, _output, new Coordinate(Convert.ToInt16(row), Convert.ToInt16(column)));
                }
                else
                {
                    character = new Monster(new Coordinate(Convert.ToInt16(row), Convert.ToInt16(column)), isOnADot == "True");
                }

                characterList.Add(character);
            }

            return new GameState(grid, score, level, livesLeft, characterList);
        }
    }
}