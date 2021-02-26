using UnityEngine;
using UnityEngine.UI;

public class ForegroundImagePanel : MonoBehaviour
{
    public bool IsReady { get; set; }
    public bool IsNextGen { get; set; }

    [SerializeField]
    private GridImagesPanel _gridImagesPanel;

    private CanvasGroup _canvasGroup;
    private RawImage _rawImage;
    private RectTransform _rectTransform;
    private RectTransform _rawImageRectTransform;
    private Vector2 _initSizeDelta;
    private Vector2 _rawImageInitSizeDelta;
    private int _textureIndex = 0;

    private void Start()
    {
        _rawImage = transform.GetChild(0).GetComponent<RawImage>();

        _rectTransform = GetComponent<RectTransform>();
        _rawImageRectTransform = _rawImage.GetComponent<RectTransform>();

        _initSizeDelta = _rectTransform.sizeDelta;
        _rawImageInitSizeDelta = _rawImageRectTransform.sizeDelta;

        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
    }

    private void Update()
    {
        if (IsReady)
        {
            IsReady = false;
            SetNextTexture();
        }
    }

    /// <summary>
    /// Resets the forefront image.
    /// </summary>
    public void ResetForefrontImage()
    {
        IsReady = false;
        _textureIndex = 0;
        _gridImagesPanel.ResetImageContainers();

        try
        {
            _canvasGroup.alpha = 0;
        }
        catch { }
    }

    /// <summary>
    /// Sets the next texture.
    /// </summary>
    private void SetNextTexture()
    {
        var t = TextureUtility.GetTexture(ref _textureIndex);
        ++_textureIndex;

        

        //(float ratio, RescaleType rescaleType) = ImageLoaderUtility.RescaleRatio(t);
        //switch (rescaleType)
        //{
        //    case RescaleType.Height:
        //        _rectTransform.sizeDelta = new Vector2(_initSizeDelta.x, _initSizeDelta.y / ratio);
        //        _rawImageRectTransform.sizeDelta = new Vector2(_rawImageInitSizeDelta.x, _rawImageInitSizeDelta.y / ratio);
        //        break;

        //    case RescaleType.Width:
        //        _rectTransform.sizeDelta = new Vector2(_initSizeDelta.x / ratio, _initSizeDelta.y);
        //        _rawImageRectTransform.sizeDelta = new Vector2(_rawImageInitSizeDelta.x / ratio, _rawImageInitSizeDelta.y);
        //        break;

        //    case RescaleType.Equal:
        //        _rectTransform.sizeDelta = _initSizeDelta;
        //        _rawImageRectTransform.sizeDelta = _rawImageInitSizeDelta;
        //        break;
        //}

        _rawImage.texture = t;
        _gridImagesPanel.SetNextTexture(t);
        _canvasGroup.alpha = 1;
    }
}
