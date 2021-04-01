using System;

public class CommandState
{
	public struct Command
    {
        public string valueOne;
        public string valueTwo;
        public int length;
        public bool state;
    }
	public static CommandState Instance = new CommandState();
    private Command currentCommand;
    public void CreateCommand(string input)
    {
        var splitedString = input.Split(new char[0]);
        currentCommand = new Command();
        if(splitedString.Length==1)
        {
            currentCommand.length = 1;
            currentCommand.valueOne = splitedString[0];
        }
        if(splitedString.Length==2)
        {
            currentCommand.length = 2;
            currentCommand.valueOne = splitedString[0];
            currentCommand.valueTwo = splitedString[1];
        }
        currentCommand.state = true;
    }
    public Command GetCommand()
    {
        return currentCommand;
    }
    public bool getState()
    {
        return currentCommand.state;
    }
}
