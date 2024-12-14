using System.Diagnostics;

if (args.Length == 0)
{
    Console.WriteLine("Usage: dotnet run {day number} [{part number}] [-t|--test] [--write-debug-data]");
    return;
}

var sw = Stopwatch.StartNew();

var output = DayUtils.RunDay(args);

Console.WriteLine(output);
Console.WriteLine("Finished in {0}ms", sw.ElapsedMilliseconds);

FileUtils.WriteOutput(args, output);