namespace AdventOfCode2024.Days;

public static class Day3
{
    public static int Part1(string[] args)
    {
        var memory = InputUtils.ParseString(args);

        var output = memory.CalculateMultiplications();

        return output;
    }

    public static int Part2(string[] args)
    {
        var memory = InputUtils.ParseString(args);

        // for the end we need to close off a 'don't'
        memory += "do()";

        var output = memory
            .Remove(@"don\'t\(\).*?do\(\)")
            .CalculateMultiplications();

        return output;
    }

    private static int CalculateMultiplications(this string memory) => memory
        .Filter(@"mul\(\d{1,3},\d{1,3}\)")
        .Select(i => i.ParseMultiplications())
        .Sum(m => m.Aggregate((a, x) => a * x));

    private static IEnumerable<int> ParseMultiplications(
        this string instruction) => instruction
            .Filter(@"\d{1,3}")
            .Select(int.Parse);
}