using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;

public class ZipFunc
{
    public void ZipExecute()
    {
        var command = CommandState.Instance.GetCommand();
        Type thisType = this.GetType();
        string methodName = command.executionFunc;
        MethodInfo methodInfo = thisType.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        methodInfo?.Invoke(this, null);
    }
    private void Zip_File()
    {
        CommandState.Command command = CommandState.Instance.GetCommand();
        string path = command.values[1];
        if (command.length < 2 || command.length > 3)
        {
            Console.WriteLine("Wrong Command");
            return;
        }
        if (!File.Exists(path))
        {
            Console.WriteLine("File is not exist");
            return;
        }
        string[] splitedPath = path.Split(".");
        if (File.Exists(splitedPath[0] + ".zip"))
        {
            Console.WriteLine("Zip File Exists");
            return;
        }
        using (var archive = ZipFile.Open(Environment.CurrentDirectory + "\\" + splitedPath[0] + ".zip", ZipArchiveMode.Create))
        {
            archive.CreateEntryFromFile(path, Path.GetFileName(path));
            if (command.length == 3)
            {
                if (command.values[2] == "-d")
                {
                    File.Delete(path);
                }
            }
        }
    }
    private void UnpackZip()
    {
        CommandState.Command command = CommandState.Instance.GetCommand();
        if (command.length != 2)
        {
            Console.WriteLine("Wrong Command");
            return;
        }
        if (!File.Exists(command.values[1]))
        {
            Console.WriteLine("Zip File is not Exists");
            return;
        }
        using (var archive = ZipFile.OpenRead(Environment.CurrentDirectory + "\\" + command.values[1]))
        {
            archive.ExtractToDirectory(Environment.CurrentDirectory);
        }
        // ZipFile.ExtractToDirectory(Environment.CurrentDirectory, command.values[1]);
    }
    private void Zip_Folder()
    {
        CommandState.Command command = CommandState.Instance.GetCommand();
        using (var archive = ZipFile.Open(Environment.CurrentDirectory + "\\" + command.values[1] + ".zip", ZipArchiveMode.Create))
        {
            string[] filePath = Directory.GetFiles(command.values[1]);
            for (int i = 0; i < filePath.Length; i++)
            {
                archive.CreateEntry(filePath[i]);
            }
        }
    }
}
