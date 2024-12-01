using AdventOfCode2024.Day1;
using AdventOfCode2024.Enums;
using AdventOfCode2024.Utils;

var (day, part, _) = args.ParseInputs();

var output = (day, part) switch
{
    (Day.Day1, Part.Part1) => Day1.Part1(args),
    (Day.Day1, Part.Part2) => Day1.Part2(args),
    _ => throw new ArgumentOutOfRangeException()
};

Console.WriteLine(output);