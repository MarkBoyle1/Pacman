using System.Collections.Generic;

namespace Pacman
{
    public class GameState
    {
        private Grid _grid;
        private int _score;
        private List<Character> _characterList;

        public GameState(Grid grid, int score, List<Character> characterList)
        {
            _grid = grid;
            _score = score;
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

        public List<Character> GetCharacterList()
        {
            return _characterList;
        }
    }
}