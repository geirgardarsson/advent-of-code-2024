namespace AdventOfCode2024.Utils;

public static class FileUtils
{
    public static List<string> ReadInput(string[] args)
    {
        var (day, _) = args.ParseInputs();

        var folder = $"Day{day}";
        var filename = "input.txt";

        if (args.HasTestFlag())
        {
            filename = $"test-{filename}";
        }

        var path = $"{folder}/{filename}";

        return [.. File.ReadAllLines(path)];
    }

    public static void WriteOutput(string[] args, object data)
    {
        var (day, part) = args.ParseInputs();

        var folder = $"Day{day}";

        var filename = args.HasTestFlag()
            ? $"part{part}-test-output.txt"
            : $"part{part}-output.txt";

        var path = $"{folder}/{filename}";

        File.WriteAllText(path, data.ToString());
    }
}