
namespace AdventOfCode2024;

public static class Day8
{
    public static int Part1(string[] args)
    {
        var map = InputUtils.ParseCharMatrix(args);

        var keys = map.GetDistinctValues();

        var antinodes = keys.ToDictionary(
            k => k,
            k => GetAllAntiNodes(map, k));

        var output = antinodes.Values
            .SelectMany(n => n)
            .Distinct()
            .Count();

        return output;
    }

    internal static int Part2(string[] args)
    {
        throw new NotImplementedException();
    }

    private static List<(int, int)> GetAllAntiNodes(char[][] map, char key)
    {
        var coordinates = map.FindCoordinates(key).ToList();

        var combinations = coordinates.GetAllCombinations().ToList();

        var antinodes = combinations
            .SelectMany(c => GetAntiNodes(c.Item1, c.Item2))
            .Where(n => !map.IsOutOfBounds(n))
            .ToList();

        return antinodes;
    }

    private static List<(int, int)> GetAntiNodes(
        (int, int) first,
        (int, int) second)
    {
        var (y1, x1) = first;
        var (y2, x2) = second;

        var diffy = Math.Abs(y1 - y2);
        var diffx = Math.Abs(x1 - x2);

        var dy1 = y1 < y2 ? y1 - diffy : y1 + diffy;
        var dy2 = y2 < y1 ? y2 - diffy : y2 + diffy;

        var dx1 = x1 < x2 ? x1 - diffx : x1 + diffx;
        var dx2 = x2 < x1 ? x2 - diffx : x1 + diffx;

        var n1 = (dy1, dx1);
        var n2 = (dy2, dx2);

        return [n1, n2];
    }
}
