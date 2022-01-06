using System.Collections.Generic;

namespace Pacman
{
    public class GameState
    {
        private Grid _grid;
        private int _score;
        private int _level;
        private int _livesLeft;
        private List<Character> _characterList;

        public GameState(Grid grid, int score, int level, int livesLeft, List<Character> characterList)
        {
            _grid = grid;
            _score = score;
            _level = level;
            _livesLeft = livesLeft;
            _characterList = characterList;
        }

        public int GetScore()
        {
            return _score;
        }

        public Grid GetGrid()
        {
            return _grid;
        }

        public int GetLevel()
        {
            return _level;
        }

        public int GetLivesLeft()
        {
            return _livesLeft;
            
        }

        public List<Character> GetCharacterList()
        {
            return _characterList;
        }
    }
}