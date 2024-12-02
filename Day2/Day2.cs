using AdventOfCode2024.Utils;

namespace AdventOfCode2024.Day2;

public static class Day2
{
    private static readonly int Limit = 3;

    public static int Part1(string[] args)
    {
        var reports = InputUtils.ParseNumberLists(args);

        var output = reports
            .Where(r => r.IsSafe())
            .Count();

        return output;
    }

    public static int Part2(string[] args)
    {
        var reports = InputUtils.ParseNumberLists(args);

        var safe = reports.Where(r => r.IsSafe()).ToList();
        var notsafe = reports.Where(r => !r.IsSafe()).ToList();

        var safeWithOneSkip = notsafe
            .Where(n => n.IsSafeWithSkip())
            .ToList();

        return safe.Count + safeWithOneSkip.Count;
    }

    private static bool IsSafe(this List<int> list) =>
        list.IsOrdered() &&
        list.IsGraduallyChanging();

    private static bool IsOrdered(this IEnumerable<int> list) =>
        list.SequenceEqual(list.OrderBy(l => l)) ||
        list.SequenceEqual(list.OrderByDescending(l => l));

    private static bool IsGraduallyChanging(this List<int> list) => list
        .SkipLast(1)
        .Select((item, i) => (item, i))
        .All(tup =>
            Math.Abs(tup.item - list[tup.i + 1]) <= Limit
            && tup.item != list[tup.i + 1]);

    private static bool IsSafeWithSkip(this List<int> list) => list
        .Where((_, i) => list
            .ExceptAtIndex(i)
            .ToList()
            .IsSafe())
        .Any();
}