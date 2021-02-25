using Newtonsoft.Json;
using System;
using System.IO;


public class IOBase<T>
{
    public virtual bool SerializeToDisk(T obj, string path)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            using (var sw = File.CreateText(path))
                sw.WriteLine(JsonConvert.SerializeObject(obj));

            return true;
        }
        catch(Exception ex)
        {
            LogUtility.Log.Exception(ex, $"Error serializing {obj.GetType()} to {path}");
            return false;
        }
    }

    public virtual T Deserialize(string path)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        }
        catch(Exception ex)
        {
            LogUtility.Log.Exception(ex, $"Error deserialzing from {path}");
            return default;
        }
    }
}
