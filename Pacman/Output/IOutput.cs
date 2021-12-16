namespace Pacman.Output
{
    public interface IOutput
    {
        public void DisplayGrid(GameState gameState);
        public void DisplayMessage(string message);
    }
}