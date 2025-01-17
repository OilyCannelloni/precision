namespace Precision.models.common;

public abstract class PositionIndexable<T>
{
    public T West { get; set; } = default!;
    public T North { get; set; } = default!;
    public T East { get; set; } = default!;
    public T South { get; set; } = default!;

    public T this[Position pos]
    {
        get => pos switch
        {
            Position.West => West,
            Position.North => North,
            Position.East => East,
            Position.South => South,
            _ => throw new ArgumentOutOfRangeException(nameof(pos), pos, null)
        };
        
        set {
            switch (pos)
            {
                case Position.West:
                    West = value;
                    break;
                case Position.North:
                    North = value;
                    break;
                case Position.East:
                    East = value;
                    break;
                case Position.South:
                    South = value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(pos), pos, null);
            }
        }
    }
}