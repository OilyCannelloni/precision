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
    
    public static IEnumerable<Position> All(this Position pos)
    {
        for (var _ = 0; _ < 4; _++)
        {
            pos = pos.Next();
            yield return pos;
        }
    }
    
}