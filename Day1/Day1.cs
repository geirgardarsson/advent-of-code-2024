namespace AdventOfCode2024.Day1;

using AdventOfCode2024.Utils;

public static class Day1
{
    public static int Part1(string[] args)
    {
        var numbers = ParseInput(args);

        var first = numbers.Select(t => t.Item1).OrderBy(i => i);
        var second = numbers.Select(t => t.Item2).OrderBy(i => i);

        var output = first
            .Zip(second, (f, s) => Math.Abs(f - s))
            .Sum();

        FileUtils.WriteOutput(args, output);

        return output;
    }

    public static int Part2(string[] args)
    {
        var numbers = ParseInput(args);

        var first = numbers.Select(t => t.Item1);

        var counter = numbers
            .Select(n => n.Item2)
            .GroupBy(n => n)
            .ToDictionary(g => g.Key, g => g.Count());

        var output = first
            .Select(n => n * (
                counter.TryGetValue(n, out int count) ? count : 0))
            .Sum();

        FileUtils.WriteOutput(args, output);

        return output;
    }

    private static IEnumerable<(int, int)> ParseInput(string[] args)
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
}
