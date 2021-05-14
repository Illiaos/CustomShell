using System;
using System.Collections.Generic;

public static class EventDataBase
{
	private static Dictionary<string, Action> zeroArgs;
	public static void RegisterEvent(string key, Action action)
    {
        if(zeroArgs == null)
        {
            zeroArgs = new Dictionary<string, Action>();
        }
        if(zeroArgs.ContainsKey(key))
        {
            return;
        }
        zeroArgs.Add(key, action);
    }
    public static void UnRegisterEvent(string key, Action action)
    {
        if(zeroArgs == null)
        {
            return;
        }
        if(!zeroArgs.ContainsKey(key))
        {
            return;
        }
        zeroArgs.Remove(key);
    }
    public static void TriggerEvent(string key)
    {
        if(zeroArgs == null)
        {
            return;
        }
        if(!zeroArgs.ContainsKey(key))
        {
            return;
        }
        zeroArgs[key]?.Invoke();
    }
}
