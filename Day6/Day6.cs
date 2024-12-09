using System.Diagnostics;

namespace AdventOfCode2024.Days;

public static class Day6
{
    public static int Part1(string[] args)
    {
        var map = InputUtils.ParseCharMatrix(args);

        var visited = new HashSet<(int, int)>();

        var (y, x) = FindStart(map);
        var direction = Direction.Up;

        try
        {
            while (true)
            {
                visited.Add((y, x));

                (y, x, direction) = Advance(map, y, x, direction);
                map[y][x] = 'X';

                if (Debugger.IsAttached)
                {
                    FileUtils.WriteMatrix(args, map, "state");
                }
            }
        }
        catch (IndexOutOfRangeException)
        {
        }

        FileUtils.WriteMatrix(args, map, "state");

        return visited.Count;
    }

    private static (int, int, Direction) Advance(char[][] map, int y, int x, Direction direction)
    {
        var nextY = FindNextY(y, direction);
        var nextX = FindNextX(x, direction);

        if (map[nextY][nextX] == '#')
        {
            direction = Rotate(direction);

            return (y, x, direction);
        }

        return (nextY, nextX, direction);
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

    private static (int, int) FindStart(char[][] map)
    {
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == '^')
                {
                    return (i, j);
                }
            }
        }

        return (-1, -1);
    }
}
