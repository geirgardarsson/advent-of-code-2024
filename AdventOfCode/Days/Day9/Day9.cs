namespace AdventOfCode2024;

public static class Day9
{
    public static int Part1(string[] args)
    {
        var memory = InputUtils.ParseString(args).ToCharArray();

        var parsed = ParseMemory(memory);

        var memoryMap = CreateMemoryMap(parsed);

        var rearranged = RearrangeMemory(parsed, memoryMap);

        var checksum = CalculateChecksum(rearranged, memoryMap);

        return checksum;
    }

    public static int Part2(string[] args)
    {
        throw new NotImplementedException();
    }

    private static List<char> ParseMemory(char[] memory)
    {
        List<char> result = [];

        var isFreeMemory = false;

        foreach (var c in memory)
        {
            var memNum = int.Parse(c.ToString());

            result.AddRange(Enumerable.Repeat(isFreeMemory
                ? '.' : c, memNum));

            isFreeMemory = !isFreeMemory;
        }

        return result;
    }

    private static Dictionary<int, int> CreateMemoryMap(List<char> memory)
    {
        Dictionary<int, int> result = [];

        int memoryId = 0;
        var blockValue = memory[0];

        for (int i = 0; i < memory.Count; i++)
        {
            var currentValue = memory[i];

            var isFreeMemory = currentValue == '.';

            if (isFreeMemory)
            {
                continue;
            }

            if (currentValue != blockValue)
            {
                blockValue = currentValue;
                memoryId++;
            }

            result[i] = memoryId;
        }

        return result;
    }

    private static List<char> RearrangeMemory(
        List<char> memory,
        Dictionary<int, int> memoryMap)
    {
        Dictionary<int, int> remapped = [];

        for (int i = memory.Count - 1; i > 0; i--)
        {
            var freeIndex = memory.FindIndex(c => c == '.');

            if (freeIndex > i)
            {
                break;
            }

            if (memory[i] == '.')
            {
                continue;
            }

            remapped[freeIndex] = memoryMap[i];

            (memory[i], memory[freeIndex]) = (memory[freeIndex], memory[i]);
        }

        memoryMap = remapped;

        return memory;
    }

    private static int CalculateChecksum(
        List<char> memory,
        Dictionary<int, int> memoryMap)
    {
        var sum = 0;

        for (int i = 0; i < memory.Count; i++)
        {
            var mem = memory[i];

            if (mem == '.')
            {
                break;
            }

            var memId = memoryMap[i];

            var val = int.Parse(mem.ToString());

            sum += memId * i;
        }

        return sum;
    }
}
