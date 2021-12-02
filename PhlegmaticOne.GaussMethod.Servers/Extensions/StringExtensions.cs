namespace PhlegmaticOne.GaussMethod.Servers.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Reruns string between two entries
    /// </summary>
    public static string Between(this string source, string from, string to)
    {
        var firstIndex = source.IndexOf(from);
        var secondIndex = source.IndexOf(to, firstIndex);
        var length = secondIndex - firstIndex;
        return length < 0 ? source.Substring(firstIndex + from.Length) :
            source.Substring(firstIndex + from.Length, length - from.Length);
    }
}