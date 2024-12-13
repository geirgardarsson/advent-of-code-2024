

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
        bool checkrecursiveNodes,
        char[][] map)
    {
        var (first, second) = combination;
        var (y1, x1) = first;
        var (y2, x2) = second;

        var diffy = Math.Abs(y1 - y2);
        var dy1 = y1 < y2 ? y1 - diffy : y1 + diffy;
        var dy2 = y2 < y1 ? y2 - diffy : y2 + diffy;

        var diffx = Math.Abs(x1 - x2);
        var dx1 = x1 < x2 ? x1 - diffx : x1 + diffx;
        var dx2 = x2 < x1 ? x2 - diffx : x2 + diffx;

        var n1 = (dy1, dx1);
        var n2 = (dy2, dx2);

        List<(int, int)> result = [n1, n2];

        if (checkrecursiveNodes && !map.IsOutOfBounds(n1) && !map.IsOutOfBounds(n2))
        {
            result.AddRange(GetAntiNodes((first, n1), true, map));
            result.AddRange(GetAntiNodes((second, n2), true, map));

            // result.AddRange(FindRecursiveAntinodes(first, n1, map));
            // result.AddRange(FindRecursiveAntinodes(second, n2, map));
        }

        return result;
    }

    private static List<(int, int)> FindRecursiveAntinodes(
        (int, int) antenna,
        (int dy1, int dx1) antinode,
        char[][] map)
    {
        List<(int, int)> extraNodes = [];

        var firstNode = antenna;
        var currentNode = antinode;

        while (true)
        {
            var (nextNode1, nextNode2) = FindNextNodes(firstNode, currentNode);

            var nextNode = nextNode1.Equals(firstNode)
                || nextNode1.Equals(currentNode)
                    ? nextNode2 : nextNode1;

            if (map.IsOutOfBounds(nextNode))
            {
                break;
            }

            extraNodes.Add(nextNode);

            firstNode = currentNode;
            currentNode = nextNode;
        }

        return extraNodes;
    }

    private static ((int, int), (int, int)) FindNextNodes(
        (int, int) first,
        (int, int) second)
    {
        var (y1, x1) = first;
        var (y2, x2) = second;

        var diffy = Math.Abs(y1 - y2);
        var dy1 = y1 < y2 ? y1 - diffy : y1 + diffy;
        var dy2 = y2 < y1 ? y2 - diffy : y2 + diffy;

        var diffx = Math.Abs(x1 - x2);
        var dx1 = x1 < x2 ? x1 - diffx : x1 + diffx;
        var dx2 = x2 < x1 ? x2 - diffx : x2 + diffx;

        var n1 = (dy1, dx1);
        var n2 = (dy2, dx2);

        return (n1, n2);
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
    }
}
