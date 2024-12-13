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

    public static IEnumerable<(int, int)> FindCoordinates(
        this char[][] map,
        params char[] matches)
    {
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                if (matches.Contains(map[i][j]))
                {
                    yield return (i, j);
                }
            }
        }
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

    public static List<char> GetDistinctValues(this char[][] map) =>
        [.. map.SelectMany(c => c).Except(['.']).Distinct()];

    public static T Middle<T>(this List<T> list) =>
        list[list.Count / 2];

    public static List<T> PadLeft<T>(this List<T> list, int totalLength, T paddingValue)
    {
        List<T> padding = [];

        while (list.Count + padding.Count < totalLength)
        {
            padding.Add(paddingValue);
        }

        return [.. padding, .. list];
    }

    public static List<T> PadRight<T>(this List<T> list, int totalLength, T paddingValue)
    {
        while (list.Count < totalLength)
        {
            list.Add(paddingValue);
        }

        return list;
    }

    public static List<(T, T)> GetAllCombinations<T>(this List<T> list)
        where T : notnull
    {
        var result = new List<(T, T)>();

        for (int i = 0; i < list.Count; i++)
        {
            for (int j = i + 1; j < list.Count; j++)
            {
                result.Add((list[i], list[j]));
            }
        }

        return result;
    }

    public static bool IsOutOfBounds<T>(
        this T[][] matrix,
        (int, int) coordinates) =>
            coordinates.Item1 < 0 ||
            coordinates.Item2 < 0 ||
            coordinates.Item1 >= matrix.Length ||
            coordinates.Item2 >= matrix[coordinates.Item1].Length;

    public static void ReplaceMap(
        this char[][] map,
        char original,
        params char[] toReplace)
    {
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                if (toReplace.Contains(map[i][j]))
                {
                    map[i][j] = original;
                }
            }
        }
    }

    public static void MarkMap(
        this char[][] map,
        char marker,
        List<(int, int)> coordinates)
    {
        foreach (var (y, x) in coordinates)
        {
            map[y][x] = marker;
        }
    }

    public static T[][] CopyMap<T>(this T[][] map)
    {
        var copy = new T[map.Length][];

        // Copy each row
        for (int i = 0; i < map.Length; i++)
        {
            if (map[i] != null)
            {
                copy[i] = new T[map[i].Length];
                Array.Copy(map[i], copy[i], map[i].Length);
            }
        }

        return copy;
    }
}