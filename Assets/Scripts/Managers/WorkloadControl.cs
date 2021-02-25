using System.Threading;
using UnityEngine;

public class WorkloadControl : MonoBehaviour
{
    [SerializeField]
    private WorkloadManager _leftWorkloadManager;

    [SerializeField]
    private WorkloadManager _rightWorkloadManager;

    /// <summary>
    /// Setup the workload panel with given comparison.
    /// </summary>
    /// <param name="c"></param>
    public void Setup(DemoConfigModel dc, ServerConfigModel sc)
    {
        LogUtility.Log.Log($"Setting up demo for {sc.ServerData.Count} systems...");

        if (sc.ServerData.Count <= 0)
        {
            LogUtility.Log.Log("ServerData count <= 0.");
            return;
        }

        _leftWorkloadManager.Setup(sc.ServerData[0], dc.StartingWorkloadLabel, sc.BaseFPS);
        _rightWorkloadManager.Setup(sc.ServerData[1], dc.StartingWorkloadLabel, sc.BaseFPS);
    }

    /// <summary>
    /// Runs the workloads.
    /// </summary>
    public void RunWorkloads(CancellationToken token)
    {
        LogUtility.Log.Log("Running workloads...");

        _leftWorkloadManager.RunDemo(token);
        _rightWorkloadManager.RunDemo(token);
    }

    public void StopWorkloads()
    {
        LogUtility.Log.Log("Stopping workloads...");

        _leftWorkloadManager.StopDemo();
        _rightWorkloadManager.StopDemo();
    }
}
