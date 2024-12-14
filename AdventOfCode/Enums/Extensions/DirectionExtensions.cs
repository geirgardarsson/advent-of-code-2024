namespace AdventOfCode2024.Enums;

public static class DirectionExtensions
{
    public static Direction RotateClockwise(this Direction dir) =>
        (Direction)((int)(dir + 1) % 4);

    public static int FindNextX(this Direction dir, int x) => dir switch
    {
        Direction.Up or Direction.Down => x,
        Direction.Right => x + 1,
        Direction.Left => x - 1,
        _ => throw new NotImplementedException(),
    };

    public static int FindNextY(this Direction dir, int y) => dir switch
    {
        Direction.Left or Direction.Right => y,
        Direction.Up => y - 1,
        Direction.Down => y + 1,
        _ => throw new NotImplementedException()
    };

}