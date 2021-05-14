using System;
using System.Collections.Generic;
using System.IO;

public class FunctionsCollection
{
    public static FunctionsCollection Instance;
    private Dictionary<string, string> directoryFunctionality = new Dictionary<string, string>();
    private Dictionary<string, string> networkFunctionality = new Dictionary<string, string>();
    private Dictionary<string, string> zipFunctionality = new Dictionary<string, string>();
    private Dictionary<string, string> dateTimeFunctionality = new Dictionary<string, string>();
    private Dictionary<string, string> processFunctionality = new Dictionary<string, string>();
    private uint dictionaryId = 0;
    private bool found = false;
    public FunctionsCollection()
    {
        ReadData("directory_func.dat", directoryFunctionality);
        ReadData("network_func.dat", networkFunctionality);
        ReadData("zip_func.dat", zipFunctionality);
        ReadData("day_func.dat", dateTimeFunctionality);
        ReadData("process_func.dat", processFunctionality);
        Instance = this;
    }
    public string GetValue(string key, uint index)
    {
        switch(index)
        {
            case 1:
                {
                    return directoryFunctionality[key];
                }
            case 2:
                {
                    return networkFunctionality[key];
                }
            case 3:
                {
                    return zipFunctionality[key];
                }
            case 4:
                {
                    return dateTimeFunctionality[key];
                }
            case 5:
                {
                    return processFunctionality[key];
                }
        }
        return "";
    }
    public bool FindCommand(string command)
    {
        found = false;
        dictionaryId = 0;
        if(!found && directoryFunctionality.ContainsKey(command))
        {
            found = true;
            dictionaryId = 1;
            return true;
        }
        else if(!found && networkFunctionality.ContainsKey(command))
        {
            found = true;
            dictionaryId = 2;
            return true;
        }
        else if(!found && zipFunctionality.ContainsKey(command))
        {
            found = true;
            dictionaryId = 3;
            return true;
        }
        else if(!found && dateTimeFunctionality.ContainsKey(command))
        {
            found = true;
            dictionaryId = 4;
            return true;
        }
        else if(!found && processFunctionality.ContainsKey(command))
        {
            found = true;
            dictionaryId = 5;
            return true;
        }
        else
        {
            dictionaryId = 0;
            found = false;
            return false;
        }
    }
    public uint GetDictionaryId()
    {
        return dictionaryId;
    }
/*    public bool FindCommand(string command)
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
    }*/
/*    public int getIndex()
    {
        if(found)
        {
            return index;
        }
        else
        {
            return -1;
        }
    }*/
/*    public int getListId()
    {
        if(found)
        {
            return listId;
        }
        else
        {
            return -1;
        }
    }*/
/*    public string getCommandDayTime(int index)
    {
        return dayTimeFunctionality[index];
    }
    public string getCommandDirectory(int index)
    {
        return directoryFunctionality[index];
    }*/
    private void ReadData(string fileName, Dictionary<string, string> dictionatyStore)
    {
        StreamReader stringReader = new StreamReader("../../../DatFiles/"+fileName);
        string line = stringReader.ReadLine();
        while (line != null)
        {
            var splitedString = line.Split(" ");
            dictionatyStore.Add(splitedString[0], splitedString[1]);
            line = stringReader.ReadLine();
        }
    }
}
