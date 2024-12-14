using System.Net.Http.Json;
using System.Text.Json;
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

    public static bool IsAny(this string str, params string[] strings) =>
        strings.Contains(str);

    public static bool HasWriteDebugDataFlag(this string[] args) =>
        args.Contains("--write-debug-data");

    public static string Serialize(this object obj) => JsonSerializer.Serialize(obj);
}