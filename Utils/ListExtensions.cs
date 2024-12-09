namespace AdventOfCode2024.Utils;

public static class ListExtensions
{
    public static bool ContainsAny<T>(
        this IEnumerable<T> list,
        params T[] arguments) => arguments.Any(a => list.Contains(a));

    public static bool IsAny<T>(this T item, params T[] list) => list.Contains(item);

    public static IEnumerable<int> ExceptAtIndex(
        this IEnumerable<int> list,
        int index) => list.Where((_, i) => i != index);

    public static T[][] RotateMatrix90Degrees<T>(this T[][] matrix)
    {
        int rows = matrix.Length;
        int columns = matrix.First().Length;

        T[][] rotated = new T[columns][];

        for (int i = 0; i < columns; i++)
        {
            rotated[i] = new T[rows];
            for (int j = 0; j < rows; j++)
            {
                rotated[i][j] = matrix[rows - 1 - j][i];
            }
        }

        return rotated;
    }


    public static List<string> RotateMatrix45DegreesLeft(this char[][] matrix)
    {
        List<string> output = [];

        int yEdge = matrix.First().Length - 1;
        int xEdge = 0;

        int yOffset = 0;

        while (yEdge >= 0)
        {
            List<char> current = [];

            int col = Math.Max(yEdge - yOffset, 0);
            int row = xEdge;

            while (col <= yEdge)
            {
                current.Add(matrix[row][col]);

                col++;
                row++;
            }

            if (current.Count > yEdge)
            {
                yEdge--;
                xEdge++;
            }

            output.Add(new string([.. current]));

            yOffset = current.Count;
        }

        return output;
    }

    public static T Middle<T>(this List<T> list) =>
        list[list.Count / 2];
}