namespace AdventOfCode2024;

public static class Day4
{
    public static int Part1(string[] args)
    {
        var matrix = InputUtils.ParseCharMatrix(args);

        List<List<string>> strings = [
            ParseVerticalStrings(matrix),
            ParseHorizontalStrings(matrix),
            ParseDiagonalStringsTopLeftToBottomRight(matrix),
            ParseDiagonalStringsBottomLeftToTopRight(matrix)
        ];

        var xmas = strings
            .SelectMany(s => s)
            .Select(s => s.Filter("XMAS").Count())
            .Sum();

        var samx = strings
            .SelectMany(s => s)
            .Select(s => s.Filter("SAMX").Count())
            .Sum();

        return xmas + samx;
    }

    public static int Part2(string[] args)
    {
        var matrix = InputUtils.ParseCharMatrix(args);

        var cols = matrix.First().Length;
        var rows = matrix.Length;

        var counter = 0;

        for (int i = 1; i < cols - 1; i++)
        {
            for (int j = 1; j < rows - 1; j++)
            {
                var topleft = matrix[i - 1][j - 1];
                var topright = matrix[i + 1][j - 1];
                var middle = matrix[i][j];
                var botleft = matrix[i - 1][j + 1];
                var botright = matrix[i + 1][j + 1];

                var x1 = new string([topleft, middle, botright]);
                var x2 = new string([topright, middle, botleft]);

                if (x1.IsAny("MAS", "SAM") && x2.IsAny("MAS", "SAM"))
                {
                    counter++;
                }
            }
        }

        return counter;
    }

    private static List<string> ParseVerticalStrings(char[][] matrix) =>
        [.. matrix
            .RotateMatrix90Degrees()
            .Select(l => new string(l))];

    private static List<string> ParseHorizontalStrings(char[][] matrix) =>
        [.. matrix.Select(l => new string(l))];

    private static List<string> ParseDiagonalStringsTopLeftToBottomRight(char[][] matrix)
        => matrix.RotateMatrix45DegreesLeft();

    private static List<string> ParseDiagonalStringsBottomLeftToTopRight(char[][] matrix) =>
        matrix
            .RotateMatrix90Degrees()
            .RotateMatrix45DegreesLeft();
}
