using AdventOfCode2024.Exceptions;

namespace AdventOfCode2024.Days;

public static class Day6
{
    public static int Part1(string[] args)
    {
        var map = InputUtils.ParseCharMatrix(args);

        map = RunMapRoute(args, map);

        var output = map.FindCoordinates('^', '-', '|', '+');

        FileUtils.WriteDebug(args, map, "state");

        return output.Count();
    }

    public static int Part2(string[] args)
    {
        var map = InputUtils.ParseCharMatrix(args);

        var loops = 0;

        map = RunMapRoute(args, map);

        var pathCoords = map.FindCoordinates('-', '|', '+').ToList();

        foreach (var (y, x) in pathCoords)
        {
            map.ReplaceMap('.', '-', '|', '+', 'O');

            map[y][x] = 'O';

            if (args.HasWriteDebugDataFlag())
            {
                FileUtils.WriteDebug(args, map, "state");
            }

            try
            {
                map = RunMapRoute(args, map);
            }
            catch (LoopException)
            {
                loops++;
            }
        }

        return loops;
    }

    private static List<(int, int, Direction)> MoveHistory = [];

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
        var (y, x) = map.FindCoordinates('^').Single();
        var dir = Direction.Up;

        MoveHistory = [];

        try
        {
            while (true)
            {
                (y, x, dir) = Advance(map, y, x, dir);

                map[y][x] = GetMapMarker(map, y, x, dir);

                if (args.HasWriteDebugDataFlag())
                {
                    FileUtils.WriteDebug(args, map, "state");
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
        var nextY = direction.FindNextY(y);
        var nextX = direction.FindNextX(x);

        if (map[nextY][nextX].IsAny('#', 'O'))
        {
            direction = direction.RotateClockwise();

            CheckForALoop(y, x, direction);

            return (y, x, direction);
        }

        return (nextY, nextX, direction);
    }

    private static void CheckForALoop(int y, int x, Direction direction)
    {
        if (MoveHistory.Contains((y, x, direction)))
        {
            throw new LoopException();
        }

        MoveHistory.Add((y, x, direction));
    }
}
