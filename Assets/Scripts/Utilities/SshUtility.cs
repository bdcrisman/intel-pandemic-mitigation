using Renci.SshNet;
using System;

public class SshUtility
{
    /// <summary>
    /// Executes the 
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="s"></param>
    /// <returns></returns>
    public static void ExecuteCommand(string ip, int port,  string user, string pwd, string cmd, int timeoutMS)
    {
        try
        {
            using (var client = new SshClient(GetConnInfo(ip, port, user, pwd, timeoutMS)))
            {
                client.Connect();
                client.CreateCommand(cmd).Execute();
                client.Disconnect();
            }
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// Executes the 
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string ResultExecuteCommand(string ip, int port, string user, string pwd, string cmd, int timeoutMS)
    {
        var result = string.Empty;

        try
        {
            using (var client = new SshClient(GetConnInfo(ip, port, user, pwd, timeoutMS)))
            {
                client.Connect();
                result = client.CreateCommand(cmd).Execute();
                client.Disconnect();
            }
        }
        catch
        {
            throw;
        }

        return result;
    }

    /// <summary>
    /// Creates new connection info.
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="user"></param>
    /// <param name="pwd"></param>
    /// <returns></returns>
    private static ConnectionInfo GetConnInfo(string ip, int port, string user, string pwd, int timeoutMS)
    {
        var connInfo = new ConnectionInfo(ip, port, user,
            new AuthenticationMethod[]
            {
                new PasswordAuthenticationMethod(user, pwd)
            });

        connInfo.Timeout = TimeSpan.FromMilliseconds(timeoutMS);

        return connInfo;
    }
}
