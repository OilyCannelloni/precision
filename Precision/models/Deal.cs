using Precision.algorithm;
using Precision.models.common;

namespace Precision.models;

public class Deal : PositionIndexable<Hand>
{
    public override string ToString()
    {
        return string.Join("  ", Position.West.All().Select(p => this[p]));
    }

    public void RemoveCard(Position pos, Card card)
    {
        this[pos][card.Suit] = this[pos][card.Suit].PopChar(card.Char());
    }
}