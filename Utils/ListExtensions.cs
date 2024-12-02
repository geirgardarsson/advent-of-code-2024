namespace AdventOfCode2024.Utils;

public static class ListExtensions
{
    public static bool ContainsAny<T>(
        this IEnumerable<T> list,
        params T[] arguments) => arguments.Any(a => list.Contains(a));

    public static IEnumerable<int> ExceptAtIndex(
        this IEnumerable<int> list,
        int index) => list.Where((_, i) => i != index);
}