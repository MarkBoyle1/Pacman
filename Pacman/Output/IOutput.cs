namespace Pacman.Output
{
    public interface IOutput
    {
        public void DisplayGrid(Grid grid);
        public void DisplayMessage(string message);
    }
}