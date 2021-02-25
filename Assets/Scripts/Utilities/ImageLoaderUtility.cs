using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageLoaderUtility
{
    /// <summary>
    /// Loads an image to a sprite.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="s"></param>
    /// <returns></returns>
    public static IEnumerator LoadImage(string path, Action<Sprite> s)
    {
        if (string.IsNullOrEmpty(path))
            yield break;

        using (var req = UnityWebRequestTexture.GetTexture(path))
        {
            yield return req.SendWebRequest();

            if (req.isNetworkError || req.isHttpError)
            {
                yield break;
            }

            var tex = DownloadHandlerTexture.GetContent(req);
            s(Sprite.Create(
                tex,
                new Rect(0, 0, tex.width, tex.height),
                new Vector2(0.5f, 0.5f)));
        }
    }

    /// <summary>
    /// Loads an image to a texture.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static IEnumerator LoadImage(string path, Action<Texture2D> t)
    {
        if (string.IsNullOrEmpty(path))
            yield break;

        using (var req = UnityWebRequestTexture.GetTexture(path))
        {
            yield return req.SendWebRequest();
            if (req.isNetworkError || req.isHttpError)
            {
                yield break;
            }
            t(DownloadHandlerTexture.GetContent(req));
        }
    }

    /// <summary>
    /// Rescales the raw image.
    /// </summary>
    /// <param name="image">Raw image to rescale.</param>
    /// <param name="width">Width of texture.</param>
    /// <param name="height">Height of texture.</param>
    public static void Rescale(RawImage image, int width, int height)
    {
        var rt = image.GetComponent<RectTransform>();

        if (width > height)
        {
            rt.localScale = new Vector3(
                rt.localScale.x,
                rt.localScale.y * ((float)height / width),
                rt.localScale.z);
        }
        else if (height > width)
        {
            rt.localScale = new Vector3(
                rt.localScale.x * ((float)width / height),
                rt.localScale.y,
                rt.localScale.z);
        }
    }

    /// <summary>
    /// Rescales the raw image.
    /// </summary>
    /// <param name="image">Raw image to rescale.</param>
    /// <param name="width">Width of texture.</param>
    /// <param name="height">Height of texture.</param>
    public static void Rescale(RectTransform rt, int width, int height)
    {
        if (width > height)
        {
            rt.localScale = new Vector3(
                rt.localScale.x,
                rt.localScale.y * ((float)height / width),
                rt.localScale.z);
        }
        else if (height > width)
        {
            rt.localScale = new Vector3(
                rt.localScale.x * ((float)width / height),
                rt.localScale.y,
                rt.localScale.z);
        }
    }

    /// <summary>
    /// Rescales the image.
    /// </summary>
    /// <param name="image">Image to rescale.</param>
    /// <param name="width">Width of the texture.</param>
    /// <param name="height">Height of the texture.</param>
    public static void Rescale(Image image, int width, int height)
    {
        var toScale = image.transform;

        if (width > height)
        {
            toScale.localScale = new Vector3(
                toScale.localScale.x,
                toScale.localScale.y * ((float)height / width),
                toScale.localScale.z);
        }
        else if (height > width)
        {
            toScale.localScale = new Vector3(
                toScale.localScale.x * ((float)width / height),
                toScale.localScale.y,
                toScale.localScale.z);
        }
    }

    public static (float ratio, RescaleType rescaleType) RescaleRatio(Texture2D t)
    {
        if (t.width > t.height)
            return ((float)t.width / t.height, RescaleType.Height);
        else if (t.height > t.width)
            return ((float)t.height / t.width, RescaleType.Width);
        else
            return (1, RescaleType.Equal);
    }

    /// <summary>
    /// Rescale and set texture.
    /// </summary>
    /// <param name="t"></param>
    public static void RescaleAndSetTexture(Texture2D t, RectTransform _rectTransform, RectTransform _rawImageRectTransform, Vector2 _initSizeDelta, Vector2 _rawImageInitSizeDelta)
    {
        if (t.width > t.height)
        {
            var ratio = (float)t.width / t.height;
            _rectTransform.sizeDelta = new Vector2(_initSizeDelta.x, _initSizeDelta.y / ratio);
            _rawImageRectTransform.sizeDelta = new Vector2(_rawImageInitSizeDelta.x, _rawImageInitSizeDelta.y / ratio);
        }
        else if (t.height > t.width)
        {
            var ratio = (float)t.height / t.width;
            _rectTransform.sizeDelta = new Vector2(_initSizeDelta.x / ratio, _initSizeDelta.y);
            _rawImageRectTransform.sizeDelta = new Vector2(_rawImageInitSizeDelta.x / ratio, _rawImageInitSizeDelta.y);
        }
        else
        {
            _rectTransform.sizeDelta = _initSizeDelta;
            _rawImageRectTransform.sizeDelta = _rawImageInitSizeDelta;
        }
    }
}

public enum RescaleType
{
    Height, 
    Width, 
    Equal
}