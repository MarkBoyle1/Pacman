using System.Collections.Generic;

namespace Pacman
{
    public class GameState
    {
        public Grid Grid { get;}
        public int Score { get;}
        public int LevelNumber { get;}
        public int LivesLeft { get;}
        public List<Character> CharacterList { get;}

        public GameState(Grid grid, int score, int levelNumber, int livesLeft, List<Character> characterList)
        {
            Grid = grid;
            Score = score;
            LevelNumber = levelNumber;
            LivesLeft = livesLeft;
            CharacterList = characterList;
        }

        public Character GetPacman()
        {
            return CharacterList.Find(x => x.GetType() == typeof(PacmanCharacter));
        }
    }
}