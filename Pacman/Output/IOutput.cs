namespace Pacman.Output
{
    public interface IOutput
    {
        public void DisplayMessage(string message);
        public void DisplayMessageWithDelay(string message);
        public void DisplayDeathAnimation(GameState gameState);
        public void SetHighScore(int highScore);
        public void DisplayEatingAnimation(GameState gameState, Grid gridWithMouthOpen, Grid gridWithMouthClosed);
        public void DisplayGameState(GameState gameState);
    }
}