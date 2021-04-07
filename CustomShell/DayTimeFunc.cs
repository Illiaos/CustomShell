using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

public class DayTimeFunc
{
    DateTime dt;

    private bool foundError = false;
    public void DayTimeExecute()
    {
        dt = DateTime.Now;
        foundError = false;
        var command = CommandState.Instance.GetCommand();
        Type thisType = this.GetType();
        string methodName = command.executionFunc;
        MethodInfo methodInfo = thisType.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        methodInfo?.Invoke(this, null);
    }

    private void DayTimeMainFunc()
    {
        CommandState.Command command = CommandState.Instance.GetCommand();
        if(command.length == 1)
        {
            Console.WriteLine(DateTime.Now);
            return;
        }
        else
        {
            string finalPrint = "";
            for(int i=1; i< command.length; i++)
            {
                finalPrint += GetFormatingData(command.values[i]) + " ";
            }
            if(foundError)
            {
                Console.WriteLine("Wrong Command");
                return;
            }
            Console.WriteLine(finalPrint);
        }
    }
    private string GetFormatingData(string key)
    {
        switch (key)
        {
            case "%A":
                {
                    return dt.DayOfWeek.ToString();
                }
            case "%D":
                {
                    return dt.Month.ToString();
                }
            case "%d":
                {
                    return dt.Day.ToString();
                }
            case "%H":
                {
                    return dt.Hour.ToString();
                }
            case "%j":
                {
                    return dt.DayOfYear.ToString();
                }
            case "%m":
                {
                    return dt.Month.ToString();
                }
            case "%M":
                {
                    return dt.Minute.ToString();
                }
            case "%S":
                {
                    return dt.Second.ToString();
                }
            case "%u":
                {
                    return dt.DayOfWeek.ToString();
                }
            case "%Y":
                {
                    return dt.Year.ToString();
                }
            default:
                {
                    foundError = true;
                    return "Wrong Command";
                }
        }

    }
}
