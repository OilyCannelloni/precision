using System.Text.Json.Serialization;

namespace Precision.game.elements.cards;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Suit
{
    Pass,
    [JsonStringEnumMemberName("Clubs")] Clubs,
    [JsonStringEnumMemberName("Diamonds")] Diamonds,
    [JsonStringEnumMemberName("Hearts")] Hearts,
    [JsonStringEnumMemberName("Spades")] Spades,
    NT
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

    public static Suit ToSuit(this char c)
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

    public static Suit Next(this Suit suit)
    {
        return suit switch
        {
            Suit.Clubs => Suit.Spades,
            Suit.Diamonds => Suit.Clubs,
            Suit.Hearts => Suit.Diamonds,
            Suit.Spades => Suit.Hearts,
            _ => throw new ArgumentOutOfRangeException(nameof(suit), suit, null)
        };
    }
    
    public static IEnumerable<Suit> OneCycle(this Suit suit)
    {
        var i = 4;
        while (i-- > 0)
        {
            yield return suit;
            suit = suit.Next();
        }
    }

    public static Suit GetRandomSuit()
    {
        return Random.Shared.Next(4) switch
        {
            0 => Suit.Spades,
            1 => Suit.Hearts,
            2 => Suit.Diamonds,
            3 => Suit.Clubs,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}