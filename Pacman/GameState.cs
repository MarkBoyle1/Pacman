using System.Collections.Generic;

namespace Pacman
{
    public class GameState
    {
        public Grid Grid { get; set; }
        public int Score { get; set; }
        public int Level { get; set; }
        public int LivesLeft { get; set; }
        public List<Character> CharacterList { get; set; }

        public GameState(Grid grid, int score, int level, int livesLeft, List<Character> characterList)
        {
            Grid = grid;
            Score = score;
            Level = level;
            LivesLeft = livesLeft;
            CharacterList = characterList;
        }

        public int GetScore()
        {
            return Score;
        }

        public Grid GetGrid()
        {
            return Grid;
        }

        public int GetLevel()
        {
            return Level;
        }

        public int GetLivesLeft()
        {
            return LivesLeft;
            
        }

        public List<Character> GetCharacterList()
        {
            return CharacterList;
        }

        public Character GetPacman()
        {
            return CharacterList.Find(x => x.GetType() == typeof(PacmanCharacter));
        }
    }
}