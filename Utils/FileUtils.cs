using AdventOfCode2024.Enums;

namespace AdventOfCode2024.Utils;

public static class FileUtils
{
    public static string[] ReadInput(string[] args)
    {
        var (day, part, isTest) = args.ParseInputs();

        var daypart = $"{day.GetDescription()}/{part.GetDescription()}";

        if (isTest)
        {
            daypart = $"{daypart}-test";
        }

        var filename = $"{daypart}-input.txt";

        return File.ReadAllLines(filename);
    }

    public static void WriteOutput(string[] args, object data)
    {
        var (day, part, isTest) = args.ParseInputs();

        var daypart = $"{day.GetDescription()}/{part.GetDescription()}";

        if (isTest)
        {
            daypart = $"{daypart}-test";
        }

        var filename = $"{daypart}-output.txt";

        File.WriteAllText(filename, data.ToString());
    }
}