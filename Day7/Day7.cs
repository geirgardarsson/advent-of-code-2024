namespace AdventOfCode2024.Days;

public static class Day7
{
    public static long Part1(string[] args)
    {
        var calibrations = InputUtils.ParseListDict<long, long>(args);

        var output = calibrations
            .Where(c => CanBeTrue(c.Key, c.Value))
            .Sum(c => c.Key);

        return output;
    }

    public static long Part2(string[] args)
    {
        var calibrations = InputUtils.ParseListDict<long, long>(args);

        var output = calibrations
            .Where(c => CanBeTrue(c.Key, c.Value, useConcatination: true))
            .Sum(c => c.Key);

        return output;
    }

    private static bool CanBeTrue(long key, List<long> numbers, bool useConcatination = false)
    {
        var permutations = GetOperationPermutations(numbers.Count, useConcatination);

        return permutations.Any(o => CalculateSequence(numbers, o) == key);
    }

    private static long CalculateSequence(List<long> numbers, List<Operations> operations)
    {
        var result = numbers.First();

        foreach (var (n, o) in numbers.Skip(1).Zip(operations))
        {
            result = o switch
            {
                Operations.Add => result + n,
                Operations.Multiply => result * n,
                Operations.Concat => int.Parse(result.ToString() + n.ToString()),
                _ => result
            };
        }

        return result;
    }

    private static List<List<Operations>> GetOperationPermutations(int numberCount, bool useConcatination)
    {
        List<Operations> allowedOperations = [Operations.Add, Operations.Multiply];

        if (useConcatination)
        {
            allowedOperations.Add(Operations.Concat);
        }

        var operations = new List<List<Operations>>();
        int operationCount = numberCount - 1;
        var operationCombinations = Math.Pow(allowedOperations.Count, operationCount);

        for (int i = 0; i < operationCombinations; i++)
        {
            var operationSet = new List<Operations>();

            var binary = Convert
                .ToString(i, allowedOperations.Count) // todo implement tertiary convertion
                .PadLeft(operationCount, '0');

            foreach (var c in binary)
            {
                operationSet.Add((Operations)int.Parse(c.ToString()));
            }

            operations.Add(operationSet);
        }

        return operations;
    }
}
