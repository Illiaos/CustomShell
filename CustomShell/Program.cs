using System;

namespace CustomShell
{
    class Program
    {
        static void Main(string[] args)
        {
            Execute execute = new Execute();
            string userInput = null;
            bool shellStatus = true;
            while(shellStatus)
            {
                Console.Write("> ");
                userInput = Console.ReadLine();
                execute.Input(userInput);
                userInput = null;
            }
        }
    }
}
