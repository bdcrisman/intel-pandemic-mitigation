using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoControl : ConfigControl
{
    const float ButtonDelay = 1f;

    public static EventHandler<StateType> StateUpdated = (s, e) => { };

    [SerializeField]
    private DemoSetup _demoSetup;

    [SerializeField]
    private WorkloadControl _workloadControl;

    [SerializeField]
    private DeltaManager _performance;

    [SerializeField]
    private LoadingManager _loadingManager;

    [SerializeField]
    private MediaControl _mediaControl;

    private CancellationTokenSource _cts;
    private StateType _curPhase = StateType.Init;
    private float _timer = 0f;
    private bool _canProgress = false;

    private void OnEnable()
    {
        _loadingManager.Finished += OnLoadingFinished;
    }

    private IEnumerator Start()
    {
        _curPhase = StateType.Init;
        _timer = 0;
        _canProgress = false;

        // allow for event initializations
        yield return new WaitForEndOfFrame();

        LoadState(StateType.Init);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F5))
        {
            SceneManager.LoadScene(0);
            return;
        }

        if (!_canProgress)
            return;

        _timer += Time.deltaTime;
        if (Input.GetKeyUp(KeyCode.Space) && _timer >= ButtonDelay)
        {
            _timer = 0f;
            LoadState(_curPhase == StateType.Loaded
                ? StateType.Scene1
                : _curPhase == StateType.Scene1
                    ? StateType.Scene2 : StateType.None);
        }
    }

    private void OnDisable()
    {
        _cts?.Cancel();
        _workloadControl.StopWorkloads();
        _performance.StopPerformance();

        _loadingManager.Finished -= OnLoadingFinished;
    }

    private void OnLoadingFinished(object sender, EventArgs e)
    {
        LoadState(StateType.Loaded);
    }

    public void NextPhase()
    {
        switch(_curPhase)
        {
            case StateType.Init:
                LoadState(StateType.Scene1);
                break;

            case StateType.Scene1:
                LoadState(StateType.Scene2);
                break;

            default:
                return;
        }
    }

    public void LoadState(StateType s)
    {
        print($"loading state: {s}");
        LogUtility.Log.Log($"Loading state: {s}");
        _curPhase = s;
        switch (s)
        {
            case StateType.Init:
                InitPhase();
                break;

            case StateType.Loaded:
                break;

            case StateType.Scene1:
                RunDemo();
                break;

            case StateType.Scene2:
                break;

            case StateType.Reload:
                ReloadDemo();
                break;

            case StateType.None:
            default:
                break;
        }

        StateUpdated(null, _curPhase);
    }

    /// <summary>
    /// Runs the demo.
    /// </summary>
    public void RunDemo()
    {
        LogUtility.Log.Log("Running demo...");
        _cts = new CancellationTokenSource();
        //RunTimerAndResetAsync(_cts.Token);

        _workloadControl.RunWorkloads(_cts.Token);
        _performance.RunPerformance(_cts.Token);
    }

    /// <summary>
    /// Resets the demo.
    /// </summary>
    public void ResetDemo()
    {
        LogUtility.Log.Log("Resetting demo...");

        _cts?.Cancel();
        _workloadControl.StopWorkloads();
        _performance.StopPerformance();
        StartCoroutine(ReInitDemo());
    }

    private void InitPhase()
    {
        LogUtility.Log.Log("Initializing demo.");

        if (!LoadConfig())
        {
            LogUtility.Log.Log("Exiting, error loading configs.");
            Application.Quit();
        }

        StartCoroutine(InitDemo());
    }

    public void ReloadDemo()
    {
        LogUtility.Log.Log("Reloading demo.");
        _cts?.Cancel();
        _workloadControl.StopWorkloads();
        _performance.StopPerformance();
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Initializes the demo.
    /// </summary>
    /// <returns></returns>
    private IEnumerator InitDemo()
    {
        if (TextureUtility.Textures.IsNullOrEmpty())
            LogUtility.Log.Log("No textures loaded.");
        else
            LogUtility.Log.Log($"{TextureUtility.Textures.Count} textures loaded.");

        _demoSetup.Setup(DemoConfig);
        _workloadControl.Setup(DemoConfig, ServerConfig);

        yield return StartCoroutine(_mediaControl.LoadClassifiedImages());

        _loadingManager.Deactivate();
        _canProgress = true;
    }

    /// <summary>
    /// Reinitializes the demo.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ReInitDemo()
    {
        LogUtility.Log.Log("Reinitializing demo...");

        try
        {
            LoadConfig();
        }
        catch (Exception e)
        {
            print(e.Message);
            Application.Quit();
            yield break;
        }

        _workloadControl.Setup(DemoConfig, ServerConfig);
        _demoSetup.Setup(DemoConfig);
    }
}
