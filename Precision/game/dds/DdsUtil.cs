using Precision.game.dds.models;
using Precision.game.elements.cards;
using Precision.game.elements.deal;

namespace Precision.game.dds;

public static class DdsUtil
{
    public static DdsSuit ToDdsSuit(this Suit suit)
    {
        return suit switch
        {
            Suit.Clubs => DdsSuit.Clubs,
            Suit.Diamonds => DdsSuit.Diamonds,
            Suit.Hearts => DdsSuit.Hearts,
            Suit.Spades => DdsSuit.Spades,
            Suit.NT => DdsSuit.NT,
            _ => throw new ArgumentOutOfRangeException(nameof(suit), suit, null)
        };
    }
    
    public static Suit ToSuit(this DdsSuit suit)
    {
        return suit switch
        {
            DdsSuit.Clubs => Suit.Clubs,
            DdsSuit.Diamonds => Suit.Diamonds,
            DdsSuit.Hearts => Suit.Hearts,
            DdsSuit.Spades => Suit.Spades,
            DdsSuit.NT => Suit.NT,
            _ => throw new ArgumentOutOfRangeException(nameof(suit), suit, null)
        };
    }

    public static DdsPosition ToDdsPosition(this Position position)
    {
        return position switch
        {
            Position.West => DdsPosition.West,
            Position.North => DdsPosition.North,
            Position.East => DdsPosition.East,
            Position.South => DdsPosition.South,
            _ => throw new ArgumentOutOfRangeException(nameof(position), position, null)
        };
    }

    public static Position ToPosition(this DdsPosition position)
    {
        return position switch
        {
            DdsPosition.North => Position.North,
            DdsPosition.East => Position.East,
            DdsPosition.South => Position.South,
            DdsPosition.West => Position.West,
            _ => throw new ArgumentOutOfRangeException(nameof(position), position, null)
        };
    }

    public static CardValue ToCardValue(this int ddsValue)
    {
        return ddsValue switch
        {
            2 => CardValue._2,
            3 => CardValue._3,
            4 => CardValue._4,
            5 => CardValue._5,
            6 => CardValue._6,
            7 => CardValue._7,
            8 => CardValue._8,
            9 => CardValue._9,
            10 => CardValue.T,
            11 => CardValue.J,
            12 => CardValue.Q,
            13 => CardValue.K,
            14 => CardValue.A,
            _ => throw new ArgumentOutOfRangeException(nameof(ddsValue), ddsValue, null)
        };
    } 

    public static (int[], int[]) ToDdsCurrentTrickRankSuit(this Trick trick)
    {
        if (trick.IsComplete())
            return ([0, 0, 0], [0, 0, 0]);

        var suits = new[] { 0, 0, 0 };
        var ranks = new[] { 0, 0, 0 };

        var i = 0;
        foreach (var pos in trick.Dealer.OneCycle())
        {
            if (trick[pos] == null)
                return (ranks, suits);
            suits[i] = (int) trick[pos]!.Suit.ToDdsSuit();
            ranks[i++] = "xx23456789TJQKA".IndexOf(trick[pos]!.Value);
        }
        return (ranks, suits);
    }

    public static int[,] ToRemainingCards(this Deal deal)
    {
        var ret = new int[4, 4];
        foreach (var pos in Enum.GetValues<DdsPosition>())
        {
            foreach (var suit in new[] {DdsSuit.Spades, DdsSuit.Hearts, DdsSuit.Diamonds, DdsSuit.Clubs})
            {
                ret[(int)pos, (int)suit] = (int) deal[pos.ToPosition()][suit.ToSuit()].Value;
            }
        }

        return ret;
    }
    
    
    public static DdsDeal ToDdsDeal(this Game game)
    {
        var (ranks, suits) = game.CurrentTrick.ToDdsCurrentTrickRankSuit();

        return new DdsDeal
        {
            Trump = game.Contract?.Suit.ToDdsSuit() ??
                    throw new ArgumentException("Cannot perform DDS analysis on game without a contract"),

            TrickDealer = game.CurrentTrick.Dealer.ToDdsPosition(),
            CurrentTrickRank = ranks,
            CurrentTrickSuit = suits,
            RemainingCards = game.CurrentDealState.ToRemainingCards()
        };
    }

    public static IEnumerable<Card> ToCards(this DdsFutureTricks futureTricks)
    {
        for (int i = 0; i < futureTricks.nCards; i++)
        {
            if (futureTricks.rank[i] == 0) 
                yield break;

            var suit = futureTricks.suit[i].ToSuit();
            var cardValue = futureTricks.rank[i].ToCardValue();
            yield return new Card(suit, cardValue);
            
            if (futureTricks.equals[i] == 0)
                continue;

            foreach (var cv in new Holding(futureTricks.equals[i]).AsCardValues())
            {
                yield return new Card(suit, cv);
            }
        }
    }
}