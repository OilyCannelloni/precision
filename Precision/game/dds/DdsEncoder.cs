using Precision.models;

namespace Precision.game;

public static class DdsEncoder
{
    public static int EncodeSuit(Suit suit)
    {
        return suit switch
        {
            Suit.Clubs => 3,
            Suit.Diamonds => 2,
            Suit.Hearts => 1,
            Suit.Spades => 0,
            Suit.NT => 4,
            _ => throw new ArgumentOutOfRangeException(nameof(suit), suit, null)
        };
    }

    public static int EncodePosition(Position pos)
    {
        return pos switch
        {
            Position.West => 3,
            Position.North => 0,
            Position.East => 1,
            Position.South => 2,
            _ => throw new ArgumentOutOfRangeException(nameof(pos), pos, null)
        };
    }

    public static int EncodeVul(Vulnerability vul)
    {
        return vul switch
        {
            Vulnerability.None => 0,
            Vulnerability.NS => 2,
            Vulnerability.EW => 3,
            Vulnerability.All => 1,
            _ => throw new ArgumentOutOfRangeException(nameof(vul), vul, null)
        };
    }

    public static int EncodeHolding(string holding)
    {
        var hi = 0;
        var ddsHand = 0;
        foreach (var cardValue in Card.Values.Reverse())
        {
            ddsHand <<= 1;
            if   (hi < holding.Length && cardValue == holding[hi])
            {
                ddsHand += 1;
                hi++;
            }
        }

        return ddsHand << 2;
    }

    public static DdsDeal EncodeDeal(Deal deal)
    {
        throw new NotImplementedException();
    }
}