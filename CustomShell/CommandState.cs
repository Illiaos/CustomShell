using System;
using System.Collections;
using System.Collections.Generic;

public class CommandState
{
    public struct Command
    {
        public string line;
        public string[] values;
        public int length;
        public bool state;
        public string executionFunc;
    }
    public static CommandState Instance = new CommandState();
    private Command currentCommand;
    private string[] history = new string[10];
    private int historyLength = 0;
    public void CreateCommand(string input)
    {
        var splitedString = input.Split(new char[0]);
        currentCommand = new Command();
        currentCommand.line = input;
        currentCommand.values = new string[splitedString.Length];
        currentCommand.values = splitedString;
        currentCommand.length = splitedString.Length;
        currentCommand.state = true;
        AddHistoryItem();
        prevSelected = -1;
        selectedHistoryItem = 0;
    }
    public Command GetCommand()
    {
        return currentCommand;
    }
    public void SetExecutionFunc(string func)
    {
        currentCommand.executionFunc = func;
    }
    private void AddHistoryItem()
    {
        if (historyLength == history.Length)
        {
            for (int i = 0; i < history.Length - 1; i++)
            {
                history[i] = history[i + 1];
            }
        }
        if(historyLength != 0)
        {
            if(currentCommand.line != history[historyLength-1])
            {
                history[historyLength] = currentCommand.line;
                historyLength++;
            }
        }
        else
        {
            history[historyLength] = currentCommand.line;
            historyLength++;
        }
    }
    private int selectedHistoryItem = 0;
    private int prevSelected = -1;
    public string GetHistoryItem(bool down)
    {
        if(prevSelected == -1)
        {
            selectedHistoryItem = historyLength;
        }
        else
        {
            selectedHistoryItem = prevSelected;
        }
        if(down)
        {
            if(prevSelected == -1)
            {
                return null;
            }
            else
            {
                selectedHistoryItem = prevSelected + 1;
                if(selectedHistoryItem >= historyLength)
                {
                    prevSelected = -1;
                    selectedHistoryItem = historyLength;
                    return "";
                }
                prevSelected = selectedHistoryItem;
                return history[selectedHistoryItem];
            }
        }
        else
        {
            selectedHistoryItem--;
            prevSelected = selectedHistoryItem;
            if(selectedHistoryItem == -1)
            {
                selectedHistoryItem = 0;
                prevSelected = 0;
                return history[selectedHistoryItem];
            }
            else
            {
                return history[selectedHistoryItem];
            }
        }
    }
}
