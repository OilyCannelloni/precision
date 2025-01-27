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

    public int Value { get; private set; }

    public bool IsEmpty()
    {
        return Value == 0;
    }

    public static int Encode(string cards)
    {
        var i = 0;
        var encoded = 0;
        foreach (var cardValue in Card.Values.Reverse())
        {
            encoded <<= 1;
            if (i < cards.Length && cardValue == cards[i])
            {
                encoded += 1;
                i++;
            }
        }

        return encoded << 2;
    }

    public bool Contains(Card card)
    {
        return (Value & card.IntValue) > 0;
    }

    public bool Contains(int cardIntValue)
    {
        return (Value & cardIntValue) > 0;
    }

    public void Remove(Card card)
    {
        Value &= ~card.IntValue;
    }

    public void Add(Card card)
    {
        Value |= card.IntValue;
    }

    public override string ToString()
    {
        var i = Card.Values.Length - 1;
        var ss = new StringBuilder();
        for (var p = 16384; p >= 4; p /= 2, i--)
            if ((Value & p) != 0)
                ss.Append(Card.Values[i]);

        return ss.ToString();
    }
}