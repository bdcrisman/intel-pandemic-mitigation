using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridImagesPanel : MonoBehaviour
{
    private List<GridImage> _gridImages;
    private int _imageIndex = 0;

    private void Start()
    {
        _gridImages = GetComponentsInChildren<GridImage>().ToList();
    }

    /// <summary>
    /// Sets the next texture.
    /// </summary>
    /// <param name="t"></param>
    public void SetNextTexture(Texture2D t)
    {
        _gridImages[_imageIndex++].SetDisplayTexture(t);

        if (_imageIndex >= _gridImages.Count)
            ResetImageContainers();
    }

    /// <summary>
    /// Resets the image containers.
    /// </summary>
    public void ResetImageContainers()
    {
        _gridImages.ForEach(x => x.Reset());
        _imageIndex = 0;
    }
}
