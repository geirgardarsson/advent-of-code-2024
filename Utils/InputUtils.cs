
namespace AdventOfCode2024.Utils;

public static class InputUtils
{
    public static List<List<int>> ParseNumberLists(
        string[] args)
    {
        var lines = FileUtils.ReadInput(args);

        var numbers = lines
            .Select(l => l
                .Split(" ")
                .Where(s => int.TryParse(s, out _))
                .Select(n => int.Parse(n))
                .ToList())
            .ToList();

        return numbers;
    }

    public static IEnumerable<(int, int)> ParseTupleList(string[] args)
    {
        var lines = FileUtils.ReadInput(args);

        var numbers = lines
            .Select(l => l
                .Split(" ")
                .Where(s => int.TryParse(s, out _))
                .Select(n => int.Parse(n))
                .ToList())
            .Select(l => (l[0], l[1]));

        return numbers;
    }

    public static string ParseString(string[] args) =>
        FileUtils.ReadInput(args).First();

    public static char[][] ParseCharMatrix(string[] args) =>
        [.. FileUtils.ReadInput(args).Select(l => l.ToCharArray())];
}