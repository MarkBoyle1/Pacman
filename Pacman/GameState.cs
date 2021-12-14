namespace Pacman
{
    public class GameState
    {
        private int _score;

        public GameState(int score)
        {
            _score = score;
        }

        public int GetScore()
        {
            return _score;
        }
    }
}