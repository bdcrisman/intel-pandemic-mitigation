using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

public class ConfigControl : MonoBehaviour
{
    protected const string DemoConfigPath = "/config/demoConfig.json";
    protected const string ServerConfigPath = "/config/serverConfig.json";

    public static EventHandler<ConfigEventArgs> ConfigObtained;

    public DemoConfigModel DemoConfig { get; private set; }
    public ServerConfigModel ServerConfig { get; private set; }

    /// <summary>
    /// Loads the config.
    /// </summary>
    protected bool LoadConfig()
    {
        try
        {
            DemoConfig = JsonConvert.DeserializeObject<DemoConfigModel>(
                File.ReadAllText(Application.streamingAssetsPath + DemoConfigPath));

            ServerConfig = JsonConvert.DeserializeObject<ServerConfigModel>(
                File.ReadAllText(Application.streamingAssetsPath + ServerConfigPath));

            OnConfigObtained();
            return true;
        }
        catch (Exception e)
        {
            LogUtility.Log.Exception(e, "Error loading configs.");
            return false;
        }
    }

    /// <summary>
    /// Raised when all the config has been obtained.
    /// </summary>
    protected void OnConfigObtained()
    {
        ConfigObtained?.Invoke(this, new ConfigEventArgs
        {
            DemoConfig = DemoConfig,
            ServerConfig = ServerConfig
        });
    }
}
