using System.Collections.Generic;

namespace Pacman
{
    public class GameState
    {
        public Grid _grid { get; set; }
        public int _score { get; set; }
        public int _level { get; set; }
        public int _livesLeft { get; set; }
        public List<Character> _characterList { get; set; }

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