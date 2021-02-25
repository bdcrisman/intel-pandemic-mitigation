using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HeroMediaContainerManager : MonoBehaviour
{
    public bool IsReady { get; set; }

    [SerializeField]
    private bool _isLeftSide;

    [SerializeField]
    private Transform[] _mediaContainers;

    private List<MultiMediaRawImageManager> _mmManagers;

    private void Start()
    {
        try
        {
            _mmManagers = new List<MultiMediaRawImageManager>();

            foreach(var m in _mediaContainers)
            {
                var containers = m.GetComponentsInChildren<MultiMediaRawImageManager>().ToList();
                _mmManagers.AddRange(containers);
            }
        }
        catch(Exception ex)
        {
            LogUtility.Log.Exception(ex, "Error getting all MultiMediaRawImageManagers");
        }
    }

    private void Update()
    {
        if (IsReady)
        {
            IsReady = false;
            SetNextTextures();
        }
    }

    /// <summary>
    /// Sets up the manager.
    /// </summary>
    /// <param name="indexSpacing"></param>
    public void Setup()
    {
        var count = TextureUtility.Textures.Count;
        var index = 0;

        LogUtility.Log.Log($"Texture count: {count}");

        if (count <= 0 || _mmManagers.Count <= 0)
            return;

        var dc = FindObjectOfType<ConfigControl>().DemoConfig;

        foreach (var m in _mmManagers)
        {
            if (index >= count)
                index -= count;

            m.Setup(index);
            index = UnityEngine.Random.Range(dc.Scene2MediaOffsetRangeMin, dc.Scene2MediaOffsetRangeMax);
        }
    }

    /// <summary>
    /// Sets the next textures.
    /// </summary>
    private void SetNextTextures()
    {
        _mmManagers.ForEach(m => m.SetNextTexture());
    }
}
