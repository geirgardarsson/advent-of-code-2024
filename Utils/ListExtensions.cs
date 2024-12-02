namespace AdventOfCode2024.Utils;

public static class ListExtensions
{
    public static bool ContainsAny<T>(
        this IEnumerable<T> list,
        params T[] arguments) => arguments.Any(a => list.Contains(a));
}