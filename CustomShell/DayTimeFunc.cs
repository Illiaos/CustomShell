using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

public class DayTimeFunc
{
    private Dictionary<string, string> commands;
    private DateTime dateTime;
    public DayTimeFunc()
    {
        commands = new Dictionary<string, string>();
        dateTime = DateTime.UtcNow.Date;
        ReadData();
    }
    public void DayTimeFuncExecute(string[] commandSplit, int commandIndex)
    {
        Type thisType = this.GetType();
        if(commands.ContainsKey(commandSplit[1]))
        {
            string methodName = commands[commandSplit[1]];
            MethodInfo theMethod = thisType.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            theMethod.Invoke(this, new object[] { commandSplit[1] });
        }
        else
        {
            Console.WriteLine("Wrong Command use help");
        }
    }
    private void GetDate(string key)
    {
        switch(key)
        {
            case "day":
                {
                    Console.WriteLine(dateTime.ToString("dd"));
                    break;
                }
            case "month":
                {
                    Console.WriteLine(dateTime.ToString("MM"));
                    break;
                }
            case "year":
                {
                    Console.WriteLine(dateTime.ToString("yyyy"));
                    break;
                }
        }
    }
    private void getCurrentDate()
    {
        Console.WriteLine(dateTime.ToString("dd/MM/yyyy"));
    }
    private void ReadData()
    {
        StreamReader stringReader = new StreamReader("../../../DatFiles/day_func.dat");
        string lines = stringReader.ReadLine();
        while(lines != null)
        {
            var splitedStrings = lines.Split(" ");
            commands.Add(splitedStrings[0], splitedStrings[1]);
            lines = stringReader.ReadLine();
        }
    }
}
