using Precision.models.common;

namespace Precision.models;

public class Trick(Position dealer) : PositionIndexable<Card?>
{
    public  Position Dealer { get; set; } = dealer;
    private Position _current = dealer;
    
    public Card? LeadCard() => this[Dealer];

    public void AddCard(Card card)
    {
        if (this[_current] != null)
            throw new IndexOutOfRangeException("Cannot add more than 4 cards to a trick");
        this[_current] = card;
        _current = _current.Next();
    }

    public bool IsComplete()
    {
        return !Position.West.OneCycle().Select(p => this[p]).Contains(null);
    }

    public Position ResolveWinner(Suit trumpSuit)
    {
        var leadSuit = this[Dealer]?.Suit ?? throw new NullReferenceException("Cannot resolve an incomplete trick");
        var bestCardPosition = Dealer;
        var bestCardValue = 0;
        foreach (var pos in Dealer.OneCycle())
        {
            var card = this[pos] ?? throw new NullReferenceException("Cannot resolve an incomplete trick");;
            if (card.Suit == leadSuit)
            {
                if (card.IntValue > bestCardValue)
                {
                    bestCardValue = card.IntValue;
                    bestCardPosition = pos;
                    
                }  
                continue;
            } 

            if (card.Suit == trumpSuit)
            {
                bestCardValue = card.IntValue;
                bestCardPosition = pos;
                leadSuit = trumpSuit;
            }
        }

        return bestCardPosition;
    }
}