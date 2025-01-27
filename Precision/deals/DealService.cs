using Precision.algorithm;
using Precision.game.elements.deal;

namespace Precision.deals;

public class DealService
{
    private readonly DealGenerator _generator = new ();

    public Deal GetRandomDeal()
    {
        return _generator.GetRandomDeal();
    }
}