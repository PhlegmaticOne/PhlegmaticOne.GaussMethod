namespace PhlegmaticOne.GaussMethod.Lib.Extensions;

public static class IEnumerableExtensions
{
    public static IEnumerable<T> Spread<T>(this IEnumerable<IEnumerable<T>> moreEnumerable)
    {
        var result = new List<T>();
        foreach (var item in moreEnumerable)
        {
            foreach (var e in item)
            {
                result.Add(e);
            }
        }

        return result;
    }
}