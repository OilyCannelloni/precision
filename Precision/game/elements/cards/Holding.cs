using System.Text;
using System.Text.Json.Serialization;
using Precision.algorithm.json;

namespace Precision.game.elements.cards;

[JsonConverter(typeof(HoldingSerializer))]
public class Holding
{
    public Holding(string cards)
    {
        Value = Encode(cards);
    }

    public CardValue Value { get; private set; }

    public bool IsEmpty()
    {
        return Value == 0;
    }

    public static CardValue Encode(string cards)
    {
        var i = 0;
        var encoded = 0;
        foreach (var cardValue in CardValueUtil.CharValues.Reverse())
        {
            encoded <<= 1;
            if (i < cards.Length && cardValue == cards[i])
            {
                encoded += 1;
                i++;
            }
        }
        return (CardValue)(encoded << 2);
    }

    public bool Contains(Card card)
    {
        return (Value & card.IntValue) > 0;
    }

    public bool Contains(CardValue cv)
    {
        return (Value & cv) > 0;
    }

    public void Remove(Card card)
    {
        Value &= ~card.IntValue;
    }
    
    public void Remove(CardValue cv)
    {
        Value &= ~cv;
    }

    public void Add(Card card)
    {
        Value |= card.IntValue;
    }

    public override string ToString()
    {
        var i = CardValueUtil.CharValues.Length - 1;
        var ss = new StringBuilder();
        for (var p = 16384; p >= 4; p /= 2, i--)
            if ((Value & (CardValue)p) != 0)
                ss.Append(CardValueUtil.CharValues[i]);

        return ss.ToString();
    }

    public IEnumerable<CardValue> AsCardValues()
    {
        for (var p = 16384; p >= 4; p /= 2)
            if ((Value & (CardValue)p) != 0)
                yield return (CardValue)p;
    }

    public CardValue PopLowest()
    {
        foreach (var cv in Enum.GetValues<CardValue>())
        {
            if ((Value & cv) != 0)
            {
                Remove(cv);
                return cv;
            }
        }

        throw new ArgumentException("PopLowest() from empty holding");
    }
}