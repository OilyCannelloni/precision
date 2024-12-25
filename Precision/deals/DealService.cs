using Precision.algorithm;
using Precision.models;

namespace Precision.deals;

public class DealService(DealGenerator generator)
{
    public Deal GetRandomDeal()
    {
        return generator.GetRandomDeal();
    }
}