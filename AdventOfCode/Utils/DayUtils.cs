namespace AdventOfCode2024.Utils;

public static class DayUtils
{
    public static long RunDay(string[] args) => args.ParseInputs() switch
    {
        (1, 1) => Day1.Part1(args),
        (1, 2) => Day1.Part2(args),
        (2, 1) => Day2.Part1(args),
        (2, 2) => Day2.Part2(args),
        (3, 1) => Day3.Part1(args),
        (3, 2) => Day3.Part2(args),
        (4, 1) => Day4.Part1(args),
        (4, 2) => Day4.Part2(args),
        (5, 1) => Day5.Part1(args),
        (5, 2) => Day5.Part2(args),
        (6, 1) => Day6.Part1(args),
        (6, 2) => Day6.Part2(args),
        (7, 1) => Day7.Part1(args),
        (7, 2) => Day7.Part2(args),
        (8, 1) => Day8.Part1(args),
        (8, 2) => Day8.Part2(args),
        _ => throw new ArgumentOutOfRangeException()
    };
}