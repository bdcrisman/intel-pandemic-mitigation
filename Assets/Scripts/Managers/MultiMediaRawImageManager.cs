using UnityEngine;
using UnityEngine.UI;

public class MultiMediaRawImageManager : MonoBehaviour
{
    public bool CanStart { get; set; }

    private RawImage _rawImage;
    private RectTransform _rectTransform;
    private Vector2 _initSizeDelta;
    private int _textureIndex = 0;

    private void Start()
    {
        _rawImage = /*transform.GetChild(0).*/GetComponent<RawImage>();
        _rectTransform = _rawImage.GetComponent<RectTransform>();
        _initSizeDelta = _rectTransform.sizeDelta;
    }

    /// <summary>
    /// Sets up the manager.
    /// </summary>
    /// <param name="startingIndex"></param>
    public void Setup(int startingIndex)
    {
        _textureIndex = startingIndex;
    }

    /// <summary>
    /// Sets the next texture.
    /// </summary>
    public void SetNextTexture()
    {
        var t = TextureUtility.GetTexture(ref _textureIndex);
        ++_textureIndex;

        //(float ratio, RescaleType rescaleType) = ImageLoaderUtility.RescaleRatio(t);
        //switch (rescaleType)
        //{
        //    case RescaleType.Height:
        //        _rectTransform.sizeDelta = new Vector2(_initSizeDelta.x, _initSizeDelta.y / ratio);
        //        break;

        //    case RescaleType.Width:
        //        _rectTransform.sizeDelta = new Vector2(_initSizeDelta.x / ratio, _initSizeDelta.y);
        //        break;

        //    case RescaleType.Equal:
        //        _rectTransform.sizeDelta = _initSizeDelta;
        //        break;
        //}

        _rawImage.texture = t;
    }
}
