using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class MediaManager : MonoBehaviour
{
    [SerializeField]
    private ForegroundImagePanel _classifiedForefrontImage;

    [SerializeField]
    private HeroMediaContainerManager _heroMediaContainer;

    private bool _isRunning = false;

    private void OnDisable()
    {
        _isRunning = false;
    }

    /// <summary>
    /// Resets the media.
    /// </summary>
    public void ResetMedia()
    {
        _isRunning = false;
    }

    /// <summary>
    /// Setup the managers.
    /// </summary>
    public void Setup()
    {
        _heroMediaContainer.Setup();
    }

    /// <summary>
    /// Runs the media loop.
    /// </summary>
    /// <param name="isFutureGen"></param>
    /// <param name="baseFPS"></param>
    /// <returns></returns>
    public async Task RunMediaLoopAsync(bool isFutureGen, float baseFPS, CancellationToken token)
    {
        if (_isRunning)
            return;
        _isRunning = true;

        var baseDelayMS = Mathf.RoundToInt((1 / baseFPS) * 1000);

        LogUtility.Log.Log($"Running media :: baseDelayMS: {baseDelayMS}...");

        while (_isRunning && !token.IsCancellationRequested)
        {
            _classifiedForefrontImage.IsReady = true;
            _heroMediaContainer.IsReady = true;

            if (isFutureGen)
            {
                await Task.Delay(baseDelayMS);
            }
            else
            {    
                var delta = Mathf.RoundToInt(DeltaManager.Delta);
                await Task.Delay(baseDelayMS * (delta > 0 ? delta : 1));
            }
        }

        LogUtility.Log.Log("End media.");
    }

    /// <summary>
    /// Stops the media.
    /// </summary>
    public void StopMedia()
    {
        LogUtility.Log.Log("Stopping media...");

        _isRunning = false;
        _classifiedForefrontImage.ResetForefrontImage();

        LogUtility.Log.Log("Media stopped.");
    }
}
