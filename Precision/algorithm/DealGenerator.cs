using Precision.game.elements.cards;
using Precision.game.elements.deal;

namespace Precision.algorithm;

public class DealGenerator
{
    public Deal GetRandomDeal()
    {
        var deck = new Deck();
        var deal = new Deal();
        foreach (var (pos, cards) in Position.West.All().Zip(deck.DealHands())) deal[pos] = Hand.FromCards(cards);

        return deal;
    }
}