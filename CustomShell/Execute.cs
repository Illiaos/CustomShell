using System;
using System.Collections.Generic;

public class Execute
{
    private DayTimeFunc dayTimeFunc = null;
    private DirectoryFunc directoryFunc = null;
    public Execute()
    {
        FunctionsCollection f = new FunctionsCollection();
        dayTimeFunc = new DayTimeFunc();
        directoryFunc = new DirectoryFunc();
    }
	public void  Input(string input)
    {
        getCurrentType(input);
    }
    private void getCurrentType(string input)
    {
        var splitedInput = input.Split(" ");
        FunctionsCollection.Instance.FindCommand(splitedInput[0]);
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
                        dayTimeFunc.DayTimeFuncExecute(splitedInput,commandId);
                        break;
                    }
                case 2:
                    {
                        directoryFunc.DirectoryFuncExecute(splitedInput);
                        break;
                    }
            }
        }
    }

}
