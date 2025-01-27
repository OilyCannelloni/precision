namespace Precision.algorithm;

public static class Util
{
    public static void Each<T>(this IEnumerable<T> ie, Action<T, int> action)
    {
        var i = 0;
        foreach (var e in ie) action(e, i++);
    }

    public static int MaxIndex(this IEnumerable<int> en)
    {
        var bestI = -1;
        var bestVal = int.MinValue;
        var i = 0;
        foreach (var el in en)
        {
            if (el > bestVal)
            {
                bestVal = el;
                bestI = i;
            }

            i++;
        }

        return bestI;
    }

    public static string PopChar(this string s, char c)
    {
        return string.Concat(s.Split(c));
    }
}