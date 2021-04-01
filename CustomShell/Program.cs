using System;
using System.Collections;
using System.Threading;

namespace CustomShell
{
    class Program
    {
        static void Main(string[] args)
        {
            Execute execute = new Execute();
            string userInput = null;
            bool shellStatus = true;
            CommandState.Command c = CommandState.Instance.GetCommand();
            while (shellStatus)
            {
                if(ThreadPool.Instance.EmptyThreadList())
                {
                    Console.Write("> ");
                    userInput = Console.ReadLine();
                    execute.Input(userInput);
                    Thread.Sleep(10);
                    userInput = null;
                    Console.WriteLine("STATE: " + c.state);
                }
            }
        }
    }
}
