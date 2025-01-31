using Precision.game.elements.cards;

namespace Precision.game.elements.bidding;

public class Bid(Suit suit, int level)
{
    public string Alert { get; set; } = string.Empty;
    public BidType Type { get; set; } = BidType.Bid;
    public int Level { get; set; } = level;
    public Suit Suit { get; set; } = suit;
}