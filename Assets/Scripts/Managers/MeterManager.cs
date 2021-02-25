using UnityEngine;
using TMPro;

public class MeterManager : MonoBehaviour
{
    [SerializeField]
    private Meter _meter;

    private MeterDataModel _meterData;
    private bool _isready = false;

    /// <summary>
    /// Sets up the meter.
    /// </summary>
    /// <param name="m"></param>
    /// <param name="displayRawData"></param>
    public void Setup(MeterDataModel m)
    {
        _meterData = m;
        Initialize();
    }

    /// <summary>
    /// Runs the meter.
    /// </summary>
    /// <param name="value"></param>
    public void RunMeter(float value)
    {
        if (_isready)
            _meter.RunMeter(value);
    }

    /// <summary>
    /// Stops the meter.
    /// </summary>
    public void StopMeter()
    {
        if (_isready)
            _meter.StopMeter();
    }

    /// <summary>
    /// Initializes the meter.
    /// </summary>
    public void Initialize()
    {
        _meter.Setup(_meterData);
        _isready = true;
    }
}
