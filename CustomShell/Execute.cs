using System;
using System.Collections.Generic;
using System.Threading;

public class Execute
{
    private DayTimeFunc dayTimeFunc = null;
    private DirectoryFunc directoryFunc = null;
    private NetworkFunc networkFunc = null;
    private ZipFunc zipFunc = null;
    private ProcessExecute processFunc = null;
    public Execute()
    {
        FunctionsCollection f = new FunctionsCollection();
        dayTimeFunc = new DayTimeFunc();
        directoryFunc = new DirectoryFunc();
        networkFunc = new NetworkFunc();
        zipFunc = new ZipFunc();
        processFunc = new ProcessExecute();
    }
    public void Input(string input)
    {
        getCurrentType(input);
    }
    private void getCurrentType(string input)
    {
        CommandState.Instance.CreateCommand(input);
        var curentState = CommandState.Instance.GetCommand();
        if (FunctionsCollection.Instance.FindCommand(curentState.values[0]))
        {
            uint id = FunctionsCollection.Instance.GetDictionaryId();
            var func = FunctionsCollection.Instance.GetValue(curentState.values[0], id);
            CommandState.Instance.SetExecutionFunc(func);
            switch (id)
            {
                case 0:
                    {
                        Console.WriteLine("Wrong Command use help");
                        break;
                    }
                case 1:
                    {
                        ThreadPool.Instance.AddTask(directoryFunc.DirectoryFuncExecute);
                        break;
                    }
                case 2:
                    {
                        ThreadPool.Instance.AddTask(networkFunc.NetworkFuncExecute);
                        break;
                    }
                case 3:
                    {
                        ThreadPool.Instance.AddTask(zipFunc.ZipExecute);
                        break;
                    }
                case 4:
                    {
                        ThreadPool.Instance.AddTask(dayTimeFunc.DayTimeExecute);
                        break;
                    }
                case 5:
                    {
                        ThreadPool.Instance.AddTask(processFunc.ProcessExecution);
                        break;
                    }
            }
        }
        else
        {
            Console.WriteLine("Wrong Command use help");
        }
        /*var currentState = CommandState.Instance.GetCommand();
        FunctionsCollection.Instance.FindCommand(currentState.values[0]);
        var listId = FunctionsCollection.Instance.getListId();
        var commandId = FunctionsCollection.Instance.getIndex();
        if (listId == -1 || commandId == -1)
        {
            Console.WriteLine("Wrong Command use help");
        }
        else
        {
            switch (listId)
            {
                case 1:
                    {
                        //dayTimeFunc.DayTimeFuncExecute(splitedInput, commandId);
                        break;
                    }
                case 2:
                    {
                        ThreadPool.Instance.AddTask(directoryFunc.DirectoryFuncExecute);
                        break;
                    }
            }
        }*/
    }
}
