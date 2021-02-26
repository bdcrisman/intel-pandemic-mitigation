using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class TextureUtility
{
    public static List<(Texture2D tex, int id)> Textures { get; private set; }

    public static void AddTexture(Texture2D t)
    {
        if (t == null)
            return;

        Textures = Textures ?? new List<(Texture2D tex, int id)>();

        int.TryParse(Regex.Replace(t.name, "[^0-9]", string.Empty), out int id);
        Textures.Add((t, id));
    }

    public static Texture2D GetTexture(ref int index)
    {
        if (Textures.IsNullOrEmpty())
        {
            return null;
        }

        if (index < 0 || index >= Textures.Count)
            index = 0;

        return Textures[index].tex;
    }

    public static void ResetTextures()
    {
        Textures = new List<(Texture2D tex, int id)>();
    }

    public static void OrderByAscending()
    {
        Textures = Textures.OrderBy(x => x.id).ToList();
    }
}
