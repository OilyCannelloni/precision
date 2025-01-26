using NUnit.Framework;
using Precision.models;

namespace PrecisionTests.cppdll;

public class TestHolding
{
    [Test]
    public void TestEncodeHolding()
    {
        string[] holdings = ["A2", "KJ4", "AQT9642"];
        int[] answers = [16388, 10256, 22100];

        for (int i = 0; i < holdings.Length; i++)
        {
            var res = new Holding(holdings[i]);
            Assert.That(res.Value, Is.EqualTo(answers[i]));
        }
    }

    [Test]
    public void TestAdd()
    {
        var holding = new Holding("AKJ853");
        holding.Add(new Card("Td"));
        Assert.That(holding.Value, Is.EqualTo(new Holding("AKJT853").Value));
        holding.Add(new Card("Kd"));
        Assert.That(holding.Value, Is.EqualTo(new Holding("AKJT853").Value));
    }

    [Test]
    public void TestRemove()
    {
        var holding = new Holding("AKJ853");
        holding.Remove(new Card("Td"));
        Assert.That(holding.Value, Is.EqualTo(new Holding("AKJ853").Value));
        holding.Remove(new Card("Kd"));
        Assert.That(holding.Value, Is.EqualTo(new Holding("AJ853").Value));
    }
}