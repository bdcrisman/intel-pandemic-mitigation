using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
[RequireComponent(typeof(CanvasGroup))]
public class GridImage : MonoBehaviour
{
    const float Alpha = 0.8f;

    private CanvasGroup _canvasGroup;
    private RawImage _img;

    // Use this for initialization
    void Start()
    {
        _img = GetComponent<RawImage>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
    }

    /// <summary>
    /// Sets and displays the texture.
    /// </summary>
    /// <param name="t"></param>
    public void SetDisplayTexture(Texture2D t)
    {
        _img.texture = t;
        _canvasGroup.alpha = Alpha;
    }

    /// <summary>
    /// Resets image being displayed.
    /// </summary>
    public void Reset()
    {
        _canvasGroup.alpha = 0;
    }

    /// <summary>
    /// Spins the image.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Spin()
    {
        var t = 0f;
        var duration = 0.25f;

        var start = transform.localEulerAngles;
        var end = new Vector3(start.x, start.y + 180, start.z);

        while (t < duration)
        {
            t += Time.deltaTime;
            transform.localEulerAngles = Vector3.Lerp(start, end, t / duration);
            yield return null;
        }

        transform.localEulerAngles = start;
    }
}
