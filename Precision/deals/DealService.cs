using Precision.algorithm;
using Precision.game.elements.deal;

namespace Precision.deals;

public class DealService(DealGenerator generator)
{
    public Deal GetRandomDeal()
    {
        return generator.GetRandomDeal();
    }
}