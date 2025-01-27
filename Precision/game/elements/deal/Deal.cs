using Precision.game.elements.cards;
using Precision.models.common;

namespace Precision.game.elements.deal;

public class Deal : PositionIndexable<Hand>
{
    public override string ToString()
    {
        return string.Join("  ", Position.West.All().Select(p => this[p]));
    }

    public void RemoveCard(Position pos, Card card)
    {
        this[pos][card.Suit].Remove(card);
    }
}