using Renci.SshNet;
using System;

public class SshUtility2
{
    private static SshClient _client;

    public static void Initialize(string ip, string user, string pwd, int timeoutMS)
    {
        try
        {
            _client = new SshClient(GetConnInfo(ip, user, pwd, timeoutMS));
            _client.Connect();

        }
        catch (Exception ex)
        {
            UnityEngine.Debug.Log(ex.ToString());
            LogUtility.Log.Exception(ex, "Error initializing ssh client connection.");
        }
    }

    public static void CloseConnection()
    {
        try
        {
            _client?.Disconnect();
            _client?.Dispose();
        }
        catch (Exception ex)
        {
            LogUtility.Log.Exception(ex, "Error closing connection to ssh client");
        }
    }

    /// <summary>
    /// Executes the given command.
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="s"></param>
    /// <returns></returns>
    public static void ExecuteCommand(string cmd)
    {
        try
        {
            _client.CreateCommand(cmd).Execute();
        }
        catch (Exception ex)
        {
            LogUtility.Log.Exception(ex, $"Error executing cmd: {cmd}");
        }
    }

    /// <summary>
    /// Executes command, returns result.
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="user"></param>
    /// <param name="pwd"></param>
    /// <param name="cmd"></param>
    /// <param name="timeoutMS"></param>
    /// <returns></returns>
    public static string ResultExecuteCommand(string cmd)
    {
        var result = string.Empty;

        try
        {
            result = _client.CreateCommand(cmd).Execute();
        }
        catch (Exception ex)
        {
            LogUtility.Log.Exception(ex, $"Error executing cmd: {cmd}");
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
    private static ConnectionInfo GetConnInfo(string ip, string user, string pwd, int timeoutMS)
    {
        var connInfo = new ConnectionInfo(ip, user,
            new AuthenticationMethod[]
            {
                new PasswordAuthenticationMethod(user, pwd)
            });

        connInfo.Timeout = TimeSpan.FromMilliseconds(timeoutMS);
        return connInfo;
    }
}
