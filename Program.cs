using AdventOfCode2024.Day1;
using AdventOfCode2024.Day2;
using AdventOfCode2024.Utils;

var (day, part) = args.ParseInputs();

object output = (day, part) switch
{
    (1, 1) => Day1.Part1(args),
    (1, 2) => Day1.Part2(args),
    (2, 1) => Day2.Part1(args),
    (2, 2) => Day2.Part2(args),
    _ => throw new ArgumentOutOfRangeException()
};

Console.WriteLine(output);