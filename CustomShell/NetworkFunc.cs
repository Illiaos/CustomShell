using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;

public class NetworkFunc
{
	public NetworkFunc()
	{

	}
	public void NetworkFuncExecute()
    {
        var command = CommandState.Instance.GetCommand();
        Type thisType = this.GetType();
        string methodName = command.executionFunc;
        MethodInfo methodInfo = thisType.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        methodInfo?.Invoke(this, null);
    }
    private void Ping()
    {
        var command = CommandState.Instance.GetCommand();
        string host = command.values[1];
        Ping p = new Ping();
        try
        {
            PingReply reply = p.Send(host, 3000);
            if (reply.Status == IPStatus.Success)
            {
                Console.WriteLine("Success Connection");
            }
            else
            {
                Console.WriteLine("Bad Connection");
            }
        }
        catch
        {
            Console.WriteLine("Wrong Host");
            return;
        }
    }
    private void HostName()
    {
        try
        {
            String hostName = Dns.GetHostName();
            Console.WriteLine(hostName);
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException caught!!!");
            Console.WriteLine("Source : " + e.Source);
            Console.WriteLine("Message : " + e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception caught!!!");
            Console.WriteLine("Source : " + e.Source);
            Console.WriteLine("Message : " + e.Message);
        }
    }
    private void Ip()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        try
        {
            foreach (var i in host.AddressList)
            {
                if (i.AddressFamily == AddressFamily.InterNetwork)
                {
                    Console.WriteLine(i.ToString());
                }
            }
        }
        catch(Exception e)
        {
            Console.WriteLine("No network adapters with an IPv4 address in the system!");
        }
    }
}
