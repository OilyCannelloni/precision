namespace Precision.algorithm;

public static class Util
{
    public static string Print<T>(this IEnumerable<T> en)
    {
        return string.Join(", ", en);
    }
}