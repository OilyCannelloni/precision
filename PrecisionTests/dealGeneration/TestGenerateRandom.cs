using NUnit.Framework;
using Precision.algorithm;

namespace PrecisionTests.dealGeneration;

[TestFixture]
public class TestGenerateRandom
{
    [TestCase]
    public void GenerateRandom_NoError()
    {
        var dg = new DealGenerator();
        var deal = dg.GetRandomDeal();
        
        Console.WriteLine(deal);
    }
}