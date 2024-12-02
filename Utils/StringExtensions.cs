namespace AdventOfCode2024.Utils;

public static class StringExtensions
{
    public static (int day, int part) ParseInputs(this string[] args)
    {
        var day = int.Parse(args[0]);
        var part = args.Length > 1
            ? int.TryParse(args[1], out int argpart) ? argpart : 1
            : 1;

        return (day, part);
    }

    public static bool HasTestFlag(this string[] args) =>
        args.ContainsAny("-t", "--test");
}