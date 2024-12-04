﻿using System.Diagnostics;
using AdventOfCode2024;
using AdventOfCode2024.Days;

var sw = Stopwatch.StartNew();

var (day, part) = args.ParseInputs();

object output = (day, part) switch
{
    (1, 1) => Day1.Part1(args),
    (1, 2) => Day1.Part2(args),
    (2, 1) => Day2.Part1(args),
    (2, 2) => Day2.Part2(args),
    (3, 1) => Day3.Part1(args),
    (3, 2) => Day3.Part2(args),
    (4, 1) => Day4.Part1(args),
    (4, 2) => Day4.Part2(args),
    _ => throw new ArgumentOutOfRangeException()
};

Console.WriteLine(output);
Console.WriteLine("Finished in {0}ms", sw.ElapsedMilliseconds);

FileUtils.WriteOutput(args, output);