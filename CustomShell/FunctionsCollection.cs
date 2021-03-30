using System;
using System.Collections.Generic;
using System.IO;

public class FunctionsCollection
{
    public static FunctionsCollection Instance;
    private List<string> dayTimeFunctionality = new List<string>();
    private List<string> directoryFunctionality = new List<string>();
    private int index = -1;
    private int listId = -1;
    private bool found = false;
    public FunctionsCollection()
    {
        dayTimeFunctionality.Add("date");
        ReadData("DirectoryFunctions.dat", directoryFunctionality);
        Instance = this;
    }
    public bool FindCommand(string command)
    {
        found = false;
        listId = -1;
        index = -1;
        if(!found && dayTimeFunctionality.Contains(command))
        {
            index = dayTimeFunctionality.IndexOf(command);
            found = true;
            listId = 1;
            return true;
        }
        else if(!found && directoryFunctionality.Contains(command))
        {
            index = directoryFunctionality.IndexOf(command);
            found = true;
            listId = 2;
            return true;
        }
        else
        {
            found = false;
            index = -1;
            listId = -1;
        }
        return false;
    }
    public int getIndex()
    {
        if(found)
        {
            return index;
        }
        else
        {
            return -1;
        }
    }
    public int getListId()
    {
        if(found)
        {
            return listId;
        }
        else
        {
            return -1;
        }
    }
    public string getCommandDayTime(int index)
    {
        return dayTimeFunctionality[index];
    }
    public string getCommandDirectory(int index)
    {
        return directoryFunctionality[index];
    }
    private void ReadData(string fileName, List<string> storeList)
    {
        StreamReader stringReader = new StreamReader("../../../ListOfFunctions/"+fileName);
        string line = stringReader.ReadLine();
        while (line != null)
        {
            storeList.Add(line);
            line = stringReader.ReadLine();
        }
    }
}
