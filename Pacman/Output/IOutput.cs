namespace Pacman.Output
{
    public interface IOutput
    {
        public void DisplayGrid(GameState gameState);
        public void DisplayMessage(string message);
        public void DisplayDeathAnimation(GameState gameState);
        public void SetHighScore(int highScore);
    }
}