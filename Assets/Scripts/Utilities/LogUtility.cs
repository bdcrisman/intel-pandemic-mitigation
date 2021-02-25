using System.IO;
using SimpleLoggerNET;

public class LogUtility
{
    public static readonly Logger Log = new Logger(Path.Combine(UnityEngine.Application.streamingAssetsPath, "logs"));
}
