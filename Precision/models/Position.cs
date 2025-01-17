namespace Precision.models;

public enum Position
{
    West, North, East, South
}


public static class PositionExtensions
{
    public static Position Next(this Position pos)
    {
        return pos switch
        {
            Position.West => Position.North,
            Position.North => Position.East,
            Position.East => Position.South,
            Position.South => Position.West,
            _ => throw new ArgumentOutOfRangeException(nameof(pos), pos, null)
        };
    }
    
    public static Position Opposite(this Position pos)
    {
        return pos switch
        {
            Position.West => Position.East,
            Position.North => Position.South,
            Position.East => Position.West,
            Position.South => Position.North,
            _ => throw new ArgumentOutOfRangeException(nameof(pos), pos, null)
        };
    }
    
    public static Position Previous(this Position pos)
    {
        return pos switch
        {
            Position.West => Position.South,
            Position.North => Position.West,
            Position.East => Position.North,
            Position.South => Position.East,
            _ => throw new ArgumentOutOfRangeException(nameof(pos), pos, null)
        };
    }
    
    public static IEnumerable<Position> All(this Position pos)
    {
        for (var _ = 0; _ < 4; _++)
        {
            pos = pos.Next();
            yield return pos;
        }
    }

    public static Position ShiftBy(this Position pos, int times)
    {
        return (times % 4) switch
        {
            0 => pos,
            1 => pos.Next(),
            2 => pos.Opposite(),
            3 => pos.Previous(),
            _ => throw new ArgumentOutOfRangeException(nameof(times), times, null)
        };
    }
}