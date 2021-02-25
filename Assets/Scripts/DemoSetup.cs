using TMPro;
using UnityEngine;

public class DemoSetup : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _mainTitle;

    [SerializeField]
    private TextMeshProUGUI _leftProductLabel;

    [SerializeField]
    private TextMeshProUGUI _rightProductLabel;

    [SerializeField]
    private TextMeshProUGUI _meterUnits;

    [SerializeField]
    private TextMeshProUGUI _loadingLabel;

    /// <summary>
    /// Sets up the demo.
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    public void Setup(DemoConfigModel d)
    {
        _mainTitle.text = d.DemoTitle;
        _leftProductLabel.text = d.LeftProductLabel;
        _rightProductLabel.text = d.RightProductLabel;
        _meterUnits.text = d.MeterUnits;
        _loadingLabel.text = d.LoadingImagesLabel;
    }
}
