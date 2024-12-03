using System.Text.RegularExpressions;

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

    public static IEnumerable<string> Filter(this string str, string pattern) =>
        Regex.Matches(str, pattern).Select(m => m.Value);

    public static string Remove(this string str, string pattern) =>
        Regex.Replace(str, pattern, "");

    public static bool HasTestFlag(this string[] args) =>
        args.ContainsAny("-t", "--test");
}