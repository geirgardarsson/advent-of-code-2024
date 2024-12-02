using AdventOfCode2024.Day1;
using AdventOfCode2024.Utils;

var (day, part) = args.ParseInputs();

var output = (day, part) switch
{
    (1, 1) => Day1.Part1(args),
    (1, 2) => Day1.Part2(args),
    _ => throw new ArgumentOutOfRangeException()
};

Console.WriteLine(output);