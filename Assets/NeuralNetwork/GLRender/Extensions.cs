using System;
using System.Collections.Generic;

public static class Extensions
{
    public static IList<TSource> ForLoop<TSource>(this IList<TSource> source, System.Action<TSource> callback)
    {
        for (int i = 0; i < source.Count; i++)
        {
            callback(source[i]);
        }
        return source;
    }
}