using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class ConfigEventArgs : EventArgs
{
    public DemoConfigModel DemoConfig { get; set; }
    public ServerConfigModel ServerConfig { get; set; }
}

public class DemoConfigModel
{
    [JsonProperty("demoTitle")]
    public readonly string DemoTitle;

    [JsonProperty("leftProductLabel")]
    public readonly string LeftProductLabel;

    [JsonProperty("RightProductLabel")]
    public readonly string RightProductLabel;

    [JsonProperty("deltaLabel")]
    public readonly string DeltaLabel;

    [JsonProperty("deltaUnits")]
    public readonly string DeltaUnits;

    [JsonProperty("meterUnits")]
    public readonly string MeterUnits;

    [JsonProperty("extraStreamsCount")]
    public readonly string ExtraStreamsCount;

    [JsonProperty("extraStreamsLabel")]
    public readonly string ExtraStreamsLabel;

    [JsonProperty("loadingImagesLabel")]
    public readonly string LoadingImagesLabel;

    [JsonProperty("startingWorkloadLabel")]
    public readonly string StartingWorkloadLabel;

    [JsonProperty("scene2MediaOffsetRangeMin")]
    public readonly int Scene2MediaOffsetRangeMin;

    [JsonProperty("scene2MediaOffsetRangeMax")]
    public readonly int Scene2MediaOffsetRangeMax;
}

public class ServerConfigModel
{
    [JsonProperty("subtitle")]
    public readonly string Subtitle;

    [JsonProperty("baseFPS")]
    public readonly float BaseFPS;

    [JsonProperty("demoTimerSeconds")]
    public readonly int DemoTimerSeconds;

    [JsonProperty("servers")]
    public readonly List<ServerDataModel> ServerData;
}

public class ServerDataModel
{
    [JsonProperty("name")]
    public readonly string Name;

    [JsonProperty("ssh")]
    public readonly SshDataModel SshData;

    [JsonProperty("meter")]
    public readonly MeterDataModel MeterData;

    [JsonProperty("flags")]
    public readonly FlagsModel Flags;

    [JsonProperty("delays")]
    public readonly DelaysDataModel Delays;

    [JsonProperty("backup")]
    public BackupDataModel BackupData { get; set; }
}

public class SshDataModel
{
    [JsonProperty("ip")]
    public readonly string IP;

    [JsonProperty("user")]
    public readonly string User;

    [JsonProperty("password")]
    public readonly string Password;

    [JsonProperty("workloadCmd")]
    public readonly string WorkloadCmd;

    [JsonProperty("logCmd")]
    public readonly string LogCmd;

    [JsonProperty("timeoutMS")]
    public readonly int TimeoutMS;
}

public class MeterDataModel
{
    [JsonProperty("max")]
    public readonly float Max;
}

public class FlagsModel
{
    [JsonProperty("isFutureGen")]
    public readonly bool IsFutureGen;

    [JsonProperty("runBackup")]
    public readonly bool RunBackup;
}

public class BackupDataModel
{
    public float Value
    {
        get
        {
            var offset = OffsetPercentage * 0.01f;
            return UnityEngine.Random.Range(Target * (1 - offset), Target);
        }
    }

    [JsonProperty("target")]
    public readonly float Target;

    [JsonProperty("offsetPercentage")]
    public readonly float OffsetPercentage;
}

public class DelaysDataModel
{
    [JsonProperty("loopDelayMS")]
    public readonly int LoopDelayMS;

    [JsonProperty("startingWorkloadDelayMS")]
    public readonly int StartingWorkloadDelayMS;
}
