using AdventOfCode2024.Utils;

namespace AdventOfCode.Tests;

public class DayTests
{
    [Theory]
    [InlineData(1, 1, 2742123)]
    [InlineData(1, 2, 21328497)]
    [InlineData(2, 1, 479)]
    [InlineData(2, 2, 531)]
    [InlineData(3, 1, 183380722)]
    [InlineData(3, 2, 82733683)]
    [InlineData(4, 1, 2547)]
    [InlineData(4, 2, 1939)]
    [InlineData(5, 1, 6951)]
    [InlineData(5, 2, 4121)]
    [InlineData(6, 1, 5208)]
    [InlineData(6, 2, 1972)]
    [InlineData(7, 1, 2437272016585)]
    [InlineData(7, 2, 162987117690649)]
    [InlineData(8, 1, 273)]
    [InlineData(8, 2, 1017)]
    public void TestDays(int day, int part, long expected)
    {
        string[] args = [
            day.ToString(),
            part.ToString(),
            "--internal-test"
        ];

        Assert.Equal(expected, DayUtils.RunDay(args));
    }
}