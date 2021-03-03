using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorkloadManager : MonoBehaviour
{
    public float CurrentValue { get; private set; }
    public bool IsFutureGen { get; private set; }

    [SerializeField]
    private MeterManager _meterManager;

    [SerializeField]
    private MediaManager _mediaManager;

    [SerializeField]
    private GameObject _loadingBenchmarkAnim;

    private BackupDataModel _backupData;
    private ServerDataModel _serverData;
    private float _baseFPS;
    private bool _isrunning = false;

    private static bool _futureGenReady = false;

    private void Start()
    {
        if (_loadingBenchmarkAnim)
            _loadingBenchmarkAnim.SetActive(false);
    }

    private void OnDisable()
    {
        _isrunning = false;
        _futureGenReady = false;
    }

    /// <summary>
    /// Sets up the workload manager.
    /// </summary>
    /// <param name="s"></param>
    public void Setup(ServerDataModel s, string startingWorkloadText, float baseFPS)
    {
        _serverData = s;
        _baseFPS = baseFPS;
        _backupData = s.BackupData;
        _meterManager.Setup(_serverData.MeterData);
        IsFutureGen = _serverData.Flags.IsFutureGen;

        if (_loadingBenchmarkAnim)
            _loadingBenchmarkAnim.GetComponentInChildren<Text>().text = startingWorkloadText;
    }

    /// <summary>
    /// Runs the demo.
    /// </summary>
    public void RunDemo(CancellationToken token)
    {
        LogUtility.Log.Log($"Running demo of {(IsFutureGen ? "future" : "previous")} gen...");

        CurrentValue = 0;
        RunDemoLoopAsync(token);
    }

    /// <summary>
    /// Stops the workload.
    /// </summary>
    public void StopDemo()
    {
        LogUtility.Log.Log($"Stopping demo of {(IsFutureGen ? "future" : "previous")} gen...");

        _isrunning = false;
        _futureGenReady = false;

        if (_loadingBenchmarkAnim)
            _loadingBenchmarkAnim.SetActive(false);

        _mediaManager.StopMedia();
        _meterManager.Initialize();
    }

    /// <summary>
    /// Runs the demo process.
    /// </summary>
    /// <returns></returns>
    private async Task RunDemoLoopAsync(CancellationToken token)
    {
        if (_isrunning)
        {
            LogUtility.Log.Log("Already running demo.");
            return;
        }
        _isrunning = true;

        // todo: loading benchmark anim
        if (_loadingBenchmarkAnim)
            _loadingBenchmarkAnim.SetActive(true);

        // Get log value
        string logValStr = await GetLogValueAsync();
        float parsedVal = ParseValue(logValStr);
        bool success = false;

        if (parsedVal > 0)
        {
            success = true;
            _serverData.BackupData.Target = parsedVal;
        }
        _serverData.BackupData.SetBackup();

        // Wait for future gen to complete
        if (_serverData.Flags.IsFutureGen)
            _futureGenReady = true;
        else
            while (!_futureGenReady)
                await Task.Delay(100, token);

        await Task.Delay(_serverData.Delays.StartingWorkloadDelayMS, token);

        if (_loadingBenchmarkAnim)
            _loadingBenchmarkAnim.SetActive(false);

        // Run the 
        RunMeter(success ? parsedVal : _serverData.BackupData.Value);

        _mediaManager.Setup(IsFutureGen);

        // Run the media animation
        Task.Run(() => _mediaManager.RunMediaLoopAsync(_serverData.Flags.IsFutureGen, _baseFPS, token), token);

        // Finally, run the workload.
        RunWorkloadAsync(token);

        while (!token.IsCancellationRequested && _isrunning)
        {
            await Task.Delay(_serverData.Delays.LoopDelayMS);
            RunMeter(_serverData.BackupData.Value);
        }
    }

    /// <summary>
    /// Runs the workload.
    /// </summary>
    /// <returns></returns>
    private async Task RunWorkloadAsync(CancellationToken token)
    {
        var s = _serverData.SshData;

        if (!_serverData.Flags.RunBackup)
        {
            try
            {
                await Task.Run(() => SshUtility.ExecuteCommand(
                    s.IP, int.Parse(s.Port), s.User, s.Password, s.WorkloadCmd, s.TimeoutMS), token);
            }
            catch (Exception e)
            {
                print($"Run workload SSH {_serverData.Name}: {e}");
            }
        }
    }

    /// <summary>
    /// Gets the log value.
    /// </summary>
    /// <returns></returns>
    private async Task<string> GetLogValueAsync()
    {
        var result = string.Empty;
        var s = _serverData.SshData;

        if (!_serverData.Flags.RunBackup)
        {
            try
            {
                result = await Task.Run(() => SshUtility.ResultExecuteCommand(
                    s.IP, int.Parse(s.Port), s.User, s.Password, s.LogCmd, s.TimeoutMS));
            }
            catch (Exception e)
            {
                print($"Get SSH log: {e}");
            }
        }

        return result;
    }

    /// <summary>
    /// Runs the given command and returns the result.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="cmd"></param>
    /// <returns></returns>
    private async Task<string> RunCmdGetResult(SshDataModel s, string cmd)
    {
        var result = string.Empty;

        try
        {
            result = await Task.Run(() => SshUtility.ResultExecuteCommand(
                s.IP, int.Parse(s.Port), s.User, s.Password, cmd, s.TimeoutMS));
        }
        catch (Exception e)
        {
            throw;
        }

        return result;
    }

    /// <summary>
    /// Custom parser to obtain value.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    private float ParseValue(string s)
    {
        if (float.TryParse(s, out var val))
            return val;
        return 0;

        //var value = 0f;

        //try
        //{
        //    return float.Parse(s.Trim()); 


        //    return value > 0 ? value : _serverData.BackupData.Value;
        //}
        //catch { }

        //return _serverData.BackupData.Value;
    }

    /// <summary>
    /// Runs the meter with the given value.
    /// </summary>
    /// <param name="value"></param>
    private void RunMeter(float value)
    {
        if (!_isrunning)
            return;

        if (value <= 0)
            value = _backupData.Value;

        if (_meterManager)
            _meterManager.RunMeter(value);

        CurrentValue = value;
    }
}
