using System.Collections.Generic;
using UnityEngine;

public class TextureUtility
{
    public static List<Texture2D> Textures { get; private set; }

    public static void AddTexture(Texture2D t, int count)
    {
        Textures = Textures ?? new List<Texture2D>(count);
        Textures.Add(t);
    }

    public static Texture2D GetTexture(ref int index)
    {
        if (Textures.IsNullOrEmpty())
        {
            return null;
        }

        if (index < 0 || index >= Textures.Count)
            index = 0;

        return Textures[index];
    }

    public static void ResetTextures()
    {
        Textures = new List<Texture2D>();
    }
}
