using System.Diagnostics;
using AdventOfCode2024.Exceptions;

namespace AdventOfCode2024.Days;

public static class Day6
{
    public static int Part1(string[] args)
    {
        var map = InputUtils.ParseCharMatrix(args);

        var visited = new HashSet<(int, int)>();

        map = RunMapRoute(args, map);

        var output = FindCoordinates(map, '^', '-', '|', '+');

        FileUtils.WriteMatrix(args, map, "state");

        return output.Count();
    }

    public static int Part2(string[] args)
    {
        var map = InputUtils.ParseCharMatrix(args);

        var loops = 0;

        map = RunMapRoute(args, map);

        var pathCoords = FindCoordinates(map, '-', '|', '+').ToList();

        foreach (var (y, x) in pathCoords)
        {
            ResetMap(map);

            map[y][x] = 'O';

            FileUtils.WriteMatrix(args, map, "state");

            try
            {
                map = RunMapRoute(args, map);
            }
            catch (LoopException)
            {
                loops++;
                Console.WriteLine("Loops detected: {0}", loops);
            }
        }

        return loops;
    }

    private static List<(int, int)> RotationHistory = [];

    private static char GetMapMarker(
        char[][] map,
        int y,
        int x,
        Direction dir)
    => (map[y][x], dir) switch
    {
        ('-', Direction.Up or Direction.Down) or
        ('|', Direction.Left or Direction.Right) => '+',

        ('.', Direction.Up or Direction.Down) => '|',

        ('.', Direction.Left or Direction.Right) => '-',

        _ => map[y][x]
    };

    private static char[][] RunMapRoute(string[] args, char[][] map)
    {
        var (y, x) = FindCoordinates(map, '^').Single();
        var dir = Direction.Up;

        RotationHistory = [];

        try
        {
            while (true)
            {
                (y, x, dir) = Advance(map, y, x, dir);

                map[y][x] = GetMapMarker(map, y, x, dir);

                if (args.HasWriteDebugDataFlag())
                {
                    FileUtils.WriteMatrix(args, map, "state");
                }
            }
        }
        catch (IndexOutOfRangeException)
        {
        }

        return map;
    }

    private static (int, int, Direction) Advance(
        char[][] map,
        int y,
        int x,
        Direction direction)
    {
        var nextY = FindNextY(y, direction);
        var nextX = FindNextX(x, direction);

        if (map[nextY][nextX].IsAny('#', 'O'))
        {
            direction = Rotate(direction);

            CheckForALoop(y, x);

            return (y, x, direction);
        }

        return (nextY, nextX, direction);
    }

    private static void CheckForALoop(int y, int x)
    {
        var historyLength = 64;

        RotationHistory.Add((y, x));

        if (RotationHistory.Count <= historyLength)
        {
            return;
        }

        RotationHistory = [.. RotationHistory.Skip(1)];

        var isLoop = RotationHistory
            .Take(historyLength / 2)
            .SequenceEqual(RotationHistory
                .Skip(historyLength / 2)
                .Take(historyLength / 2));

        if (isLoop)
        {
            throw new LoopException();
        }
    }

    private static int FindNextX(int x, Direction direction) => direction switch
    {
        Direction.Up or Direction.Down => x,
        Direction.Right => x + 1,
        Direction.Left => x - 1,
        _ => throw new NotImplementedException(),
    };

    private static int FindNextY(int y, Direction direction) => direction switch
    {
        Direction.Left or Direction.Right => y,
        Direction.Up => y - 1,
        Direction.Down => y + 1,
        _ => throw new NotImplementedException()
    };

    private static Direction Rotate(Direction direction)
        => (Direction)((int)(direction + 1) % 4);

    private static IEnumerable<(int, int)> FindCoordinates(
        char[][] map,
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

    private static void ResetMap(char[][] map)
    {
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                if (map[i][j].IsAny('-', '|', '+', 'O'))
                {
                    map[i][j] = '.';
                }
            }
        }
    }
}
