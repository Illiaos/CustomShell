using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

public class ProcessExecute
{
    private List<string> dictionaryOfProcesses;
    //private Dictionary<string, Process> dictionaryOfProcesses;

    public void ProcessExecution()
    {
        var command = CommandState.Instance.GetCommand();
        Type thisType = this.GetType();
        string methodName = command.executionFunc;
        MethodInfo methodInfo = thisType.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        methodInfo?.Invoke(this, null);
    }
    private void RunProcess()
    {
        CommandState.Command command = CommandState.Instance.GetCommand();
        if(command.length == 2)
        {
            try
            {
                if(dictionaryOfProcesses == null)
                {
                    dictionaryOfProcesses = new List<string>();
                }
                if (dictionaryOfProcesses.Contains(command.values[1]))
                {
                    Console.WriteLine("Proccess Already Exist");
                    return;
                }
                using var proccess = Process.Start(command.values[1]);
                dictionaryOfProcesses.Add(command.values[1]);
            }
            catch(Exception e)
            {
                Console.WriteLine("Wrong Process Name");
            }
        }
        else
        {
            Console.WriteLine("Wrong Command");
        }
    }
    private void KillProcess()
    {
        if(dictionaryOfProcesses == null)
        {
            return;
        }
        CommandState.Command command = CommandState.Instance.GetCommand();
        if(command.length == 2)
        {
            if(dictionaryOfProcesses.Contains(command.values[1]))
            {
                Process[] process = Process.GetProcessesByName(command.values[1]);
                if(process.Length>0)
                {
                    process[0].Kill();
                    dictionaryOfProcesses.Remove(command.values[1]);
                }
            }
            else
            {
                Console.WriteLine("Process does not exist");
            }
        }
        else
        {
            Console.WriteLine("Wrong Command");
        }
    }
}
