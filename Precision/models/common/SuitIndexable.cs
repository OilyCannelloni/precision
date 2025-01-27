using Precision.game.elements.cards;

namespace Precision.models.common;

public abstract class SuitIndexable<T>
{
    public T Spades { get; set; } = default!;
    public T Hearts { get; set; } = default!;
    public T Diamonds { get; set; } = default!;
    public T Clubs { get; set; } = default!;

    public T this[Suit suit]
    {
        get => suit switch
        {
            Suit.Pass or Suit.NT => throw new ArgumentException($"Cannot reference Hand by Suit {suit}"),
            Suit.Clubs => Clubs,
            Suit.Diamonds => Diamonds,
            Suit.Hearts => Hearts,
            Suit.Spades => Spades,
            _ => throw new ArgumentOutOfRangeException(nameof(suit), suit, null)
        };

        set
        {
            switch (suit)
            {
                case Suit.Pass or Suit.NT:
                    throw new ArgumentException($"Cannot reference Hand by Suit {suit}");
                case Suit.Clubs:
                    Clubs = value;
                    break;
                case Suit.Diamonds:
                    Diamonds = value;
                    break;
                case Suit.Hearts:
                    Hearts = value;
                    break;
                case Suit.Spades:
                    Spades = value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(suit), suit, null);
            }
        }
    }

    protected IEnumerable<T> AllSubValues()
    {
        yield return this[Suit.Spades];
        yield return this[Suit.Hearts];
        yield return this[Suit.Diamonds];
        yield return this[Suit.Clubs];
    }

    public T RandomSubValue()
    {
        return Random.Shared.Next(4) switch
        {
            0 => Spades,
            1 => Hearts,
            2 => Diamonds,
            3 => Clubs,
            _ => throw new ArgumentException("Invalid Index")
        };
    }
}