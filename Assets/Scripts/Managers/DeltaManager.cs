using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;

public class DeltaManager : MonoBehaviour
{
    public static float Delta { get; private set; }

    [SerializeField]
    private WorkloadManager _leftWorkloadManager;

    [SerializeField]
    private WorkloadManager _rightWorkloadManager;

    [SerializeField]
    private TextMeshProUGUI _deltaText;

    [SerializeField]
    private TextMeshProUGUI _label;

    private string _units;
    private bool _isrunning = false;

    private void Awake()
    {
        _deltaText.text = string.Empty;
        _label.text = string.Empty;
    }

    private void OnEnable()
    {
        ConfigControl.ConfigObtained += OnConfigObtained;
    }

    private void OnDisable()
    {
        _isrunning = false;
        ConfigControl.ConfigObtained -= OnConfigObtained;
    }

    private void OnStateUpdated(object sender, StateType state)
    {
        switch(state)
        {
            case StateType.Reload:
                break;
        }
    }

    /// <summary>
    /// Raised when the demo config has been obtained.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnConfigObtained(object sender, ConfigEventArgs e)
    {
        if (e.ServerConfig.ServerData.Count <= 1)
        {
            gameObject.SetActive(false);
            return;
        }

        _deltaText.text = string.Empty;
        _label.text = e.DemoConfig.DeltaLabel;
        _units = e.DemoConfig.DeltaUnits;
    }

    /// <summary>
    /// Runs the performance.
    /// </summary>
    public void RunPerformance(CancellationToken token)
    {
        if (_isrunning)
            return;

        _isrunning = true;
        StartCoroutine(CalculatePerformanceLoop(token));
    }

    /// <summary>
    /// Stops the performance
    /// </summary>
    public void StopPerformance()
    {
        _isrunning = false;
    }

    /// <summary>
    /// Calculates the performance.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CalculatePerformanceLoop(CancellationToken token)
    {
        if (_leftWorkloadManager.IsFutureGen)
        {
            while (_isrunning && !token.IsCancellationRequested)
            {
                var leftVal = _leftWorkloadManager.CurrentValue;
                var rightVal = _rightWorkloadManager.CurrentValue;
                var val = rightVal > 0 ? leftVal / rightVal : 0;

                if (val != Delta)
                {
                    Delta = val;
                }

                _deltaText.text = string.Format("{0:n2}{1}", Delta, _units);

                yield return null;
            }
        }
        else if (_rightWorkloadManager.IsFutureGen)
        {
            while (_isrunning)
            {
                var leftVal = _leftWorkloadManager.CurrentValue;
                var rightVal = _rightWorkloadManager.CurrentValue;
                var perf = leftVal > 0 ? rightVal / leftVal : 0;

                if (perf != Delta)
                {
                    Delta = perf;
                }

                _deltaText.text = string.Format("{0:n2}{1}", Delta, _units);

                yield return null;
            }
        }

        OnPerformanceEnded();
    }

    private void OnPerformanceEnded()
    {
        _deltaText.text = string.Empty;
        Delta = 0;
    }
}
