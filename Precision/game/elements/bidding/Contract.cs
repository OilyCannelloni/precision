using Precision.game.elements.cards;

namespace Precision.game.elements.bidding;

public class Contract
{
    public Suit Suit { get; set; }
    public int Level { get; set; }
    public BidType Doubled { get; set; } = BidType.Pass;
}