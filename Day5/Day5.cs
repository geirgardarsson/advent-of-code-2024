
namespace AdventOfCode2024;

public static class Day5
{
    public static int Part1(string[] args)
    {
        var (rules, updates) = ParseInputs(args);

        var output = updates
            .Where(u => u.IsOrdered(rules))
            .Select(u => u.Middle())
            .Sum();

        return output;
    }

    public static int Part2(string[] args)
    {
        var (rules, updates) = ParseInputs(args);

        var output = updates
            .Where(u => !u.IsOrdered(rules))
            .Select(u => u.Reorder(rules))
            .Select(u => u.Middle())
            .Sum();

        return output;
    }

    private static bool IsOrdered(
        this List<int> updates,
        Dictionary<int, List<int>> rules)
    {
        foreach (var step in updates)
        {
            if (!rules.TryGetValue(step, out var stepRules))
            {
                continue;
            }

            var overlap = updates
                .Take(updates.IndexOf(step))
                .Intersect(stepRules);

            if (overlap.Any())
            {
                return false;
            }
        }

        return true;
    }

    private static List<int> Reorder(
        this List<int> updates,
        Dictionary<int, List<int>> rules)
    {
        var currentRules = new Dictionary<int, int>();

        foreach (var update in updates)
        {
            if (!rules.ContainsKey(update))
            {
                currentRules[update] = 0;
                continue;
            }

            currentRules[update] = rules[update]
                .Intersect(updates)
                .Count();
        }

        var reordered = updates
            .OrderByDescending(u => currentRules[u])
            .ToList();

        return reordered;
    }

    private static (Dictionary<int, List<int>>, List<List<int>>) ParseInputs(
        string[] args)
    {
        var (rulesData, updatesData) = InputUtils.ParseSplitInput(args);

        var rules = ParseRules(rulesData);
        var updates = InputUtils.ParseNumberLists(updatesData, ",");

        return (rules, updates);
    }

    private static Dictionary<int, List<int>> ParseRules(
        List<string> rules)
    {
        var result = new Dictionary<int, List<int>>();

        foreach (var rule in rules)
        {
            var split = rule.Split("|");
            var first = int.Parse(split[0]);
            var second = int.Parse(split[1]);

            if (!result.TryGetValue(first, out var seconds))
            {
                seconds = [];
                result[first] = seconds;
            }

            seconds.Add(second);
        }

        return result;
    }
}
