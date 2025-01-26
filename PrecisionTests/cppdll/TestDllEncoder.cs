using NUnit.Framework;
using Precision.game;

namespace PrecisionTests.cppdll;

public class TestDllEncoder
{
    [Test]
    public void TestEncodeHolding()
    {
        string[] holdings = ["A2", "KJ4", "AQT9642"];
        int[] answers = [16388, 10256, 22100];

        for (int i = 0; i < holdings.Length; i++)
        {
            var res = DdsEncoder.EncodeHolding(holdings[i]);
            Assert.That(res, Is.EqualTo(answers[i]));
        }
    }
}