using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Meter : MonoBehaviour
{
    const float InterpolationDuration = 0.25f;

    public float CurrentValue { get; private set; }

    [SerializeField]
    private TextMeshProUGUI _valueText;

    [SerializeField]
    private Image _fill;

    private MeterDataModel _meterData;
    private float _currentValue;
    private bool _isrunning = false;

    private void Awake()
    {
        _fill.fillAmount = 0f;
        _valueText.text = string.Empty;
    }

    private void OnDisable()
    {
        _isrunning = false;
    }

    /// <summary>
    /// Setup the meter.
    /// </summary>
    /// <param name="m"></param>
    public void Setup(MeterDataModel m)
    {
        _meterData = m;
    }

    /// <summary>
    /// Runs the meter.
    /// </summary>
    /// <param name="util"></param>
    /// <param name="isNewVMs"></param>
    public void RunMeter(float value)
    {
        if (_isrunning)
            return;

        _isrunning = true;
        StartCoroutine(InterpolateToValue(value));
    }

    /// <summary>
    /// Stops the meter.
    /// </summary>
    public void StopMeter()
    {
        _isrunning = false;
    }

    /// <summary>
    /// Interpolate to the given value..
    /// </summary>
    /// <param name="util"></param>
    /// <param name="isNewVMs"></param>
    /// <returns></returns>
    private IEnumerator InterpolateToValue(float value)
    {
        var t = 0f;
        var valueStart = _currentValue;
        var valueEnd = value;
        var fillStart = _fill.fillAmount;
        var fillEnd = value / _meterData.Max;

        CurrentValue = value;

        while (t < InterpolationDuration)
        {
            t += Time.deltaTime;
            var timeRatio = t / InterpolationDuration;
            var fill = Mathf.Lerp(fillStart, fillEnd, timeRatio);
            _fill.fillAmount = fill;
            _currentValue = Mathf.Lerp(valueStart, valueEnd, timeRatio);
            _valueText.text = _currentValue.ToString("0.00");

            yield return null;
        }

        _fill.fillAmount = fillEnd;
        _currentValue = value;

        _isrunning = false;
    }
}
