using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using System.Windows.Input;
namespace CustomShell
{
    class Program
    {
        private static int accum = 0;
        static void Main(string[] args)
        {
            EventDataBase.RegisterEvent("PRINT_LABEL", PrintLabel);
            Execute execute = new Execute();
            string userInput = null;
            bool shellStatus = true;
            int currentIndex = 0;
            CommandState.Command c = CommandState.Instance.GetCommand();
            PrintLabel();
            while (shellStatus)
            {
                if (ThreadPool.Instance.EmptyThreadList())
                {/*
                    if (accum == 0)
                    {
                        PrintLabel();
                        accum += 1;
                    }*/
                    ConsoleKeyInfo readKey = Console.ReadKey(true);
                    if (readKey.Key == ConsoleKey.Enter)
                    {
                        if (userInput != null)
                        {
                            Console.WriteLine();
                            execute.Input(userInput);
                            currentIndex = 0;
                            accum = 0;
                        }
                        //Thread.Sleep(10);
                        userInput = null;
                    }
                    else if (readKey.Key == ConsoleKey.Backspace)
                    {
                        if (currentIndex > 0)
                        {
                            userInput = userInput.Remove(userInput.Length - 1);
                            Console.Write(readKey.KeyChar);
                            Console.Write(' ');
                            Console.Write(readKey.KeyChar);
                            currentIndex--;
                        }
                    }
                    else if (readKey.Key == ConsoleKey.UpArrow)
                    {
                        ClearCurrentConsoleLine();
                        var swapString = CommandState.Instance.GetHistoryItem(false);
                        if (swapString != null)
                        {
                            userInput = swapString;
                            currentIndex = userInput.Length;
                            Console.Write("> " + swapString);
                        }
                    }
                    else if (readKey.Key == ConsoleKey.DownArrow)
                    {
                        ClearCurrentConsoleLine();
                        var swapString = CommandState.Instance.GetHistoryItem(true);
                        if (swapString != null)
                        {
                            userInput = swapString;
                            currentIndex = userInput.Length;
                            Console.Write("> " + swapString);
                        }
                    }
                    else
                    {
                        userInput += readKey.KeyChar;
                        Console.Write(readKey.KeyChar);
                        currentIndex++;
                    }
                }
            }
        }
        private static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
        private static void PrintLabel ()
        {
            Console.Write("> ");
        }
        ~Program()
        {
            EventDataBase.UnRegisterEvent("PRINT_LABEL", PrintLabel);
        }
    }
}
