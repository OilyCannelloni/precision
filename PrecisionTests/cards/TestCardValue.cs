using NUnit.Framework;
using Precision.game.elements.cards;

namespace PrecisionTests.cards;

public class TestCardValue
{
    [Test]
    public void Test_FromChar()
    {
        var chars = "26A";
        var answers = new[] {CardValue._2, CardValue._6, CardValue.A};
        for(var i = 0; i < chars.Length; i++)
        {
            var cv = chars[i].ToCardValue();
            Assert.That(cv, Is.EqualTo(answers[i]));
        }
    }
    
    [Test]
    public void Test_ToChar()
    {
        var cvs = new[] {CardValue._2, CardValue._6, CardValue.A};
        var answers = "26A";
        for(var i = 0; i < cvs.Length; i++)
        {
            var c = cvs[i].ToChar();
            Assert.That(c, Is.EqualTo(answers[i]));
        }
    }
}