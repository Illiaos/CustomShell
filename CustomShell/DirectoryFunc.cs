using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;

public class DirectoryFunc
{
    /*public static void CreateDirectory(string name)
    {
        string currentPath = Directory.GetCurrentDirectory();
        //Console.WriteLine("PATH: " + currentPath);
        if(Directory.Exists(currentPath+"/"+name))
        {
            Console.WriteLine("EXIST");
        }
    }
    public static void DeleteFolder(string path)
    {
        if(Directory.Exists(path))
        {
            Directory.Delete(path);
        }
        else
        {
            Console.WriteLine($"Wrong path: {path}");
        }
    }
    public static void CheckExistOfFolder(string path)
    {
        if(DirectoryExist(path))
        {
            Console.WriteLine($"Directory Exist: {path}");
        }
        else
        {
            Console.WriteLine($"Directory is not Exist: {path}");
        }
    }
    private static bool DirectoryExist(string path)
    {
        if(Directory.Exists(path))
        {
            return true;
        }
        return false;
    }
*//*    public static void GetCreationTime(string path)
    {
        if(!DirectoryExist(path))
        {
            Console.WriteLine($"Directory is not Exist: {path}");
            return;
        }
        DateTime dt = Directory.GetCreationTime(path);
        Console.WriteLine($"Creation Time: {dt}");
    }*/
    /*    public static void GetCurrentDirectory()
        {
            string path = Directory.GetCurrentDirectory();
            Console.WriteLine($"PATH: {path}");
        }*/
    /*    public static void OpenDirectory(string path)
        {

        }*//*
        public static void OpenStartDirectory()
        {
            Environment.CurrentDirectory = @"c:\";
        }*/
    private Dictionary<string, string> commandsDirectory;
    public DirectoryFunc()
    {
        commandsDirectory = new Dictionary<string, string>();
        ReadData();
    }
    public void Data(object a)
    {

    }
    public void DirectoryFuncExecute()
    {
        var command = CommandState.Instance.GetCommand();
        Type thisType = this.GetType();
        string methodName = commandsDirectory[command.valueOne];
        MethodInfo methodInfo = thisType.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        MethodInfo genericMethod = methodInfo.MakeGenericMethod(typeof(string));
        if (command.length == 1)
        {
            genericMethod.Invoke(this, new object[] { null });
        }
        else if (command.length == 2)
        {
            genericMethod.Invoke(this, new object[] { command.valueTwo });
        }
        else
        {
            Console.WriteLine("Wrong Command");
        }
    }
    private void OpenFolder<T>(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            Environment.CurrentDirectory = @"c:\";
            return;
        }
        else
        {
            string path = Environment.CurrentDirectory + "\\" + name;
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Folder is not Exist");
                return;
            }
            Environment.CurrentDirectory = path;
        }
    }
    private void CreateFolder<T>(string name)
    {
        string path = Environment.CurrentDirectory;
        if (Directory.Exists(path + "/" + name))
        {
            return;
        }
        Directory.CreateDirectory(path + "/" + name);
    }
    private void ListOfFiles<T>(string path)
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
    private void CurrentLocation<T>(string path)
    {
        Console.WriteLine(Environment.CurrentDirectory);
    }
    private void ReadData()
    {
        StreamReader stringReader = new StreamReader("../../../DatFiles/directory_func.dat");
        string lines = stringReader.ReadLine();
        while (lines != null)
        {
            var splitedStrings = lines.Split(" ");
            commandsDirectory.Add(splitedStrings[0], splitedStrings[1]);
            lines = stringReader.ReadLine();
        }
    }

}
