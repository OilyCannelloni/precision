using Precision.algorithm;
using Precision.models.common;

namespace Precision.game.elements.cards;

public class Hand : SuitIndexable<Holding>
{
    public IEnumerable<Holding> Holdings()
    {
        yield return Spades;
        yield return Hearts;
        yield return Diamonds;
        yield return Clubs;
    }

    public override string ToString()
    {
        return string.Join(".", Holdings());
    }

    public bool IsEmpty() => Holdings().All(h => h.IsEmpty());

    public static Hand FromCards(ArraySegment<int> cards)
    {
        if (cards.Count != 13)
            throw new ArgumentException("Invalid number of cards in a hand.");

        // Array [0..4] of List<char>()
        var store = Enumerable.Range(0, 4).Select(_ => new List<char>()).ToArray();
        foreach (var card in cards)
        {
            if (card is < 0 or >= 52)
                throw new ArgumentException("Invalid card value.");
            store[card / 13].Add("23456789TJQKA"[card % 13]);
        }

        // TODO: optimize
        return new Hand
        {
            Spades = new Holding(string.Join("", store[0].OrderBy(c => -c.CardValue()))),
            Hearts = new Holding(string.Join("", store[1].OrderBy(c => -c.CardValue()))),
            Diamonds = new Holding(string.Join("", store[2].OrderBy(c => -c.CardValue()))),
            Clubs = new Holding(string.Join("", store[3].OrderBy(c => -c.CardValue())))
        };
    }

    public bool ContainsCard(Card card)
    {
        return this[card.Suit].Contains(card);
    }

    public IEnumerable<Card> AsCards()
    {
        foreach (var suit in Suit.Spades.OneCycle())
        {
            var suitCards = this[suit].AsCardValues().Select(cv => new Card(suit, cv));
            foreach (var card in suitCards)
                yield return card;
        }
    }

    public void RemoveCard(Card card)
    {
        this[card.Suit].Remove(card);
    }

    public Card PopLowestFrom(Suit suit)
    {
        var cv = this[suit].PopLowest();
        return new Card(suit, cv);
    }
}