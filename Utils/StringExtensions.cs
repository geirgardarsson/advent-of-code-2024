using AdventOfCode2024.Enums;

namespace AdventOfCode2024.Utils;

public static class StringExtensions
{
    public static (Day, Part, bool) ParseInputs(this string[] args)
    {
        var day = (Day)int.Parse(args[0]);
        var part = (Part)int.Parse(args[1]);
        var test = args.Length > 2 && args[2] == "test";

        return (day, part, test);
    }
}