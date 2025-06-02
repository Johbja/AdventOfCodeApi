namespace AdventOfCode.Application.Extensions;

public static class CollectionExtensions
{
    public static void ForEach<T>(this IEnumerable<T> values, Action<T> action)
    {
        if (values == null) return;

        foreach (var value in values)
        {
            action?.Invoke(value);
        }
    }

    public static T[] WrapInArray<T>(this T item) => new[] { item };

    public static void AddIfNotNull<T>(this IList<T> list, T item) where T : class
    {
        if (list == null) return;
        if (item == null) return;

        list.Add(item);
    }
}
