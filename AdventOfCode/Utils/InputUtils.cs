namespace AdventOfCode2024.Utils;

public static class InputUtils
{
    public static List<List<int>> ParseNumberLists(string[] args)
        => ParseNumberLists(FileUtils.ReadInput(args));

    public static List<List<int>> ParseNumberLists(
        List<string> lines,
        string delimiter = " ") => [.. lines
            .Select(l => l
                .Split(delimiter)
                .Where(s => int.TryParse(s, out _))
                .Select(n => int.Parse(n))
                .ToList())];

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

    public static Dictionary<TKey, List<TValue>> ParseListDict<TKey, TValue>(
        string[] args) where TKey : notnull
    {
        var lines = FileUtils.ReadInput(args);

        return lines
            .Select(l => l.Split(":"))
            .ToDictionary(
                s => (TKey)Convert.ChangeType(s.First(), typeof(TKey)),
                s => s
                    .Last()
                    .Trim()
                    .Split(" ")
                    .Select(n => (TValue)Convert.ChangeType(n, typeof(TValue)))
                    .ToList());
    }

    public static string ParseString(string[] args) =>
        FileUtils.ReadInput(args).First();

    public static char[][] ParseCharMatrix(string[] args) =>
        [.. FileUtils.ReadInput(args).Select(l => l.ToCharArray())];

    public static (List<string> list1, List<string> list2) ParseSplitInput(
        string[] args)
    {
        var lines = FileUtils.ReadInput(args).ToList();

        var splitIndex = lines.IndexOf("");

        var first = lines.Take(splitIndex).ToList();
        var second = lines.Skip(splitIndex + 1).ToList();

        return (first, second);
    }
}