using System.Diagnostics.CodeAnalysis;

namespace Precision.models;

public enum Suit
{
    Pass, Clubs, Diamonds, Hearts, Spades, NT
}

public static class SuitExtensions
{
    public static bool IsGreaterThan(this Suit @this, Suit other)
    {
        if (@this == other)
            return false;
        foreach (var suit in Enum.GetValues<Suit>())
        {
            if (suit == @this)
                return true;
            if (suit == other)
                return false;
        }
        throw new ArgumentException($"Invalid comparison: {@this} > {other}");
    }

    public static Suit FromChar(char c)
    {
        return char.ToLower(c) switch
        {
            'c' => Suit.Clubs,
            'd' => Suit.Diamonds,
            'h' => Suit.Hearts,
            's' => Suit.Spades,
            'n' => Suit.NT,
            'p' => Suit.Pass,
            _ => throw new ArgumentException($"Invalid suit char: {c}")
        };
    }

    public static char ToChar(this Suit suit)
    {
        return suit switch
        {
            Suit.Pass => 'p',
            Suit.Clubs => 'c',
            Suit.Diamonds => 'd',
            Suit.Hearts => 'h',
            Suit.Spades => 's',
            Suit.NT => 'n',
            _ => throw new ArgumentOutOfRangeException(nameof(suit), suit, null)
        };
    }
}
