using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;

public class DirectoryFunc
{
    public void DirectoryFuncExecute()
    {
        var command = CommandState.Instance.GetCommand();
        Type thisType = this.GetType();
        string methodName = command.executionFunc;
        MethodInfo methodInfo = thisType.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        methodInfo?.Invoke(this,null);
    }
    private void MoveUp()
    {
        CommandState.Command command = CommandState.Instance.GetCommand();
        if(command.length == 1)
        {
            var path = Directory.GetParent(Environment.CurrentDirectory);
            Environment.CurrentDirectory = path.ToString();
        }
        else
        {
            Console.WriteLine("Wrong Command");
            return;
        }
    }
    private void OpenFolder()
    {
        CommandState.Command command = CommandState.Instance.GetCommand();
        if (/*string.IsNullOrWhiteSpace(command.values[1]) ||*/ command.length == 1)
        {
            Environment.CurrentDirectory = @"c:\";
            return;
        }
        else
        {
            string path = Environment.CurrentDirectory + "\\" + command.values[1];
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Folder is not Exist");
                return;
            }
            Environment.CurrentDirectory = path;
        }
    }
    private void CreateFolder()
    {
        CommandState.Command command = CommandState.Instance.GetCommand();
        string path = Environment.CurrentDirectory;
        if (Directory.Exists(path + "/" + command.values[1]))
        {
            return;
        }
        Directory.CreateDirectory(path + "/" + command.values[1]);
    }
    private void ListOfFiles()
    {
        var listOfDir = Directory.GetDirectories(Environment.CurrentDirectory);
        var listOfFiles = Directory.GetFiles(Environment.CurrentDirectory);
        foreach (var i in listOfDir)
        {
            Console.WriteLine(i);
        }
        foreach (var i in listOfFiles)
        {
            Console.WriteLine(i);
        }
        return;
    }
    private void CurrentLocation()
    {
        Console.WriteLine(Environment.CurrentDirectory);
    }
    private void RenameFolder()
    {
        CommandState.Command command = CommandState.Instance.GetCommand();
        string path = Environment.CurrentDirectory + "\\" + command.values[1];
        if (!Directory.Exists(path))
        {
            Console.WriteLine("Folder is not Exist");
            return;
        }
        Directory.Move(command.values[1], command.values[2]);
    }
    private void RemoveFolder()
    {
        CommandState.Command command = CommandState.Instance.GetCommand();
        string path = Environment.CurrentDirectory + "\\" + command.values[1];
        if(!Directory.Exists(path))
        {
            Console.WriteLine("Folder is not Exist");
            return;
        }
        Directory.Delete(path);
    }
}
