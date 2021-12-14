using System;

namespace Pacman.Input
{
    public class UserInput : IUserInput
    {
        public string GetUserInput()
        {
            return Console.ReadLine();
        }
    }
}