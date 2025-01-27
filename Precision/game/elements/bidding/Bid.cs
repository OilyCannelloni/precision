using Precision.game.elements.cards;

namespace Precision.game.elements.bidding;

public class Bid
{
    public string Alert = string.Empty;
    public BidType Type { get; set; }
    public int Level { get; set; }
    public Suit Suit { get; set; }
}