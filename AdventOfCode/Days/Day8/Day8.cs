namespace AdventOfCode2024;

public static class Day8
{
    public static int Part1(string[] args)
        => CalculateAntinodes(args);

    internal static int Part2(string[] args)
        => CalculateAntinodes(args, true);

    private static int CalculateAntinodes(
        string[] args,
        bool checkrecursiveNodes = false)
    {
        var map = InputUtils.ParseCharMatrix(args);

        var keys = map.GetDistinctValues();

        var antinodes = keys.ToDictionary(
            k => k,
            k => GetAllAntiNodes(map, k, checkrecursiveNodes));

        if (args.HasWriteDebugDataFlag())
        {
            WriteDebugState(args, map, antinodes);
        }

        var output = antinodes.Values
            .SelectMany(n => n)
            .Distinct()
            .Count();

        return output;
    }

    private static List<(int, int)> GetAllAntiNodes(
        char[][] map,
        char key,
        bool checkrecursiveNodes)
    {
        var coordinates = map.FindCoordinates(key).ToList();

        var combinations = coordinates.GetAllCombinations().ToList();

        var antinodes = combinations
            .SelectMany(c => GetAntiNodes(c, checkrecursiveNodes, map))
            .Where(n => !map.IsOutOfBounds(n))
            .ToList();


        return antinodes;
    }

    private static List<(int, int)> GetAntiNodes(
        ((int, int), (int, int)) combination,
        bool findNodeChain,
        char[][] map)
    {
        var (first, second) = combination;

        if (!findNodeChain)
        {
            var n1 = FindNextAntinode(first, second);
            var n2 = FindNextAntinode(second, first);

            return [n1, n2];
        }

        return [
            first,
            second,
            .. FindAntinodeChain(first, second, map),
            .. FindAntinodeChain(second, first, map)];
    }

    private static List<(int, int)> FindAntinodeChain(
        (int, int) first,
        (int, int) second,
        char[][] map)
    {
        List<(int, int)> results = [];

        while (true)
        {
            var nextnode = FindNextAntinode(first, second);

            if (map.IsOutOfBounds(nextnode))
            {
                break;
            }

            results.Add(nextnode);

            first = second;
            second = nextnode;
        }

        return results;
    }

    private static (int, int) FindNextAntinode(
        (int, int) first,
        (int, int) second)
    {
        var (y1, x1) = first;
        var (y2, x2) = second;

        var diffy = Math.Abs(y1 - y2);
        var diffx = Math.Abs(x1 - x2);

        var dy1 = y1 < y2 ? y2 + diffy : y2 - diffy;
        var dx1 = x1 < x2 ? x2 + diffx : x2 - diffx;

        return (dy1, dx1);
    }

    private static void WriteDebugState(
        string[] args,
        char[][] map,
        Dictionary<char, List<(int, int)>> antinodes)
    {
        foreach (var (key, nodes) in antinodes)
        {
            var copy = map.CopyMap();

            copy.ReplaceMap('.', [.. antinodes.Keys.Except([key])]);

            copy.MarkMap('#', nodes);

            FileUtils.WriteDebug(args, copy, key.ToString());
        }

        var tcopy = map.CopyMap();

        tcopy.MarkMap('#', [.. antinodes.Values.SelectMany(n => n)]);

        FileUtils.WriteDebug(args, tcopy, "all");

    }
}
