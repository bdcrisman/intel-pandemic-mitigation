using System.Collections.Generic;
using System.Linq;

public static class ListExtensions 
{
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
    {
        if (list == null)
            return true;

        return !list.Any();
    }
}
