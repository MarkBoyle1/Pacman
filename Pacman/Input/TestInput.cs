using Microsoft.VisualBasic;

namespace Pacman.Input
{
    public class TestInput : IUserInput
    {
        private string _input;
        public TestInput(string input)
        {
            _input = input;
        }
        public string GetUserInput()
        {
            return _input;
        }
    }
}