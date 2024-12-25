namespace Precision.models;

public class Deal
{
    public Hand West { get; set; } = null!;
    public Hand North { get; set; } = null!;
    public Hand East { get; set; } = null!;
    public Hand South { get; set; } = null!;

    public override string ToString()
    {
        return string.Join("  ", Position.West.All().Select(p => this[p]));
    }

    public Hand this[Position pos]
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