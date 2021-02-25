using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _progress;

    [SerializeField]
    private Image _fill;

    private void OnEnable()
    {
        _fill.fillAmount = 0;
        _progress.text = string.Empty;
    }

    public void Set(float v)
    {
        _fill.fillAmount = v;
        _progress.text = $"{Mathf.RoundToInt(v * 100)}%";
    }
}
