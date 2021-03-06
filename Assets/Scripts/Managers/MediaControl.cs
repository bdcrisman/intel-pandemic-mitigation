﻿using System;
using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;

public class MediaControl : MonoBehaviour
{
    private readonly string ImagesDir = Path.Combine(Application.streamingAssetsPath, "media", "images", "classified");

    [SerializeField]
    private LoadingBar _loadingBar;

    private static int _texturesToLoadCount;

    private void OnDisable()
    {
        if (!TextureUtility.Textures.IsNullOrEmpty() && TextureUtility.Textures.Count != _texturesToLoadCount)
        {
            TextureUtility.ResetTextures();
        }
    }

    /// <summary>
    /// Sets up the media with the given server data config model.
    /// </summary>
    /// <param name="s"></param>
    public IEnumerator LoadClassifiedImages()
    {
        var paths = Directory.GetFiles(ImagesDir, "*.*", SearchOption.TopDirectoryOnly)
            .Where(path => !path.ToLower().EndsWith(".meta") && path.ToLower().EndsWith(".png") || path.ToLower().EndsWith(".jpg"))
            .ToList();

        if (paths == null)
            yield break;

        var loadedCount = 1;
        _texturesToLoadCount = paths.Count;

        if (!TextureUtility.Textures.IsNullOrEmpty() && TextureUtility.Textures.Count == _texturesToLoadCount)
        {
            LogUtility.Log.Log("Skipping as textures already loaded."); 
        }
        else
        {
            TextureUtility.ResetTextures();
            foreach (var path in paths)
            {
                yield return StartCoroutine(LoadImage(path));

                _loadingBar.Set((float)loadedCount / _texturesToLoadCount);
                ++loadedCount;
            }
            Resources.UnloadUnusedAssets();
            TextureUtility.OrderByAscending();
        }
    }

    /// <summary>
    /// Loads an image to a texture.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    private IEnumerator LoadImage(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            yield break;
        }
        var t = new Texture2D(1, 1);
        yield return StartCoroutine(ImageLoaderUtility.LoadImage(path, f => t = f));
        if (t == null)
        {
            yield break;
        }

        t.Compress(true);
        t.name = Path.GetFileName(path);
        TextureUtility.AddTexture(t);
    }
}
