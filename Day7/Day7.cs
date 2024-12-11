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

        var outcomes = permutations.Select(p => CalculateSequence(numbers, p)).ToList();

        return outcomes.Any(o => o == key);
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
                Operations.Concat => long.Parse($"{result}{n}"),
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
            var currentOperations = new List<Operations>();

            var n = i;

            while (n > 0)
            {
                var remainder = n % allowedOperations.Count;
                currentOperations.Add((Operations)remainder);
                n /= allowedOperations.Count;
            }

            currentOperations.PadRight(operationCount, Operations.Add);

            operations.Add(currentOperations);
        }

        return operations;
    }
}
