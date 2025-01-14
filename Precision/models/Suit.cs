using System.Diagnostics.CodeAnalysis;

namespace Precision.models;

public struct Suit : IComparable<Suit>, IEquatable<Suit>
{
    private int InternalValue { get; set; }

    public static readonly int Pass = -1;
    public static readonly int Clubs = 0;
    public static readonly int Diamonds = 1;
    public static readonly int Hearts = 2;
    public static readonly int Spades = 3;
    public static readonly int NT = 4;

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        var other = (Suit?)obj;
        return other?.InternalValue.Equals(this.InternalValue) ?? false;
    }

    public override int GetHashCode()
    {
        return InternalValue.GetHashCode();
    }

    public static bool operator <(Suit left, Suit right)
    {
        return left.InternalValue < right.InternalValue;
    }

    public static bool operator >(Suit left, Suit right)
    {
        return left.InternalValue > right.InternalValue;
    }

    public static bool operator ==(Suit left, Suit right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Suit left, Suit right)
    {
        return !(left == right);
    }

    public int CompareTo(Suit other)
    {
        return InternalValue.CompareTo(other.InternalValue);
    }

    public bool Equals(Suit other)
    {
        return InternalValue == other.InternalValue;
    }
}
