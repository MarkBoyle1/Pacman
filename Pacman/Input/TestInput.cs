using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace Pacman.Input
{
    public class TestInput : IUserInput
    {
        private List<string> _input;
        public TestInput(List<string> input)
        {
            _input = input;
        }
        public string GetUserInput()
        {
            string move = _input[0];
            _input.RemoveAt(0);
            return move;
        }
    }
}