using Swan;

namespace Precision.game.elements.cards;

[Flags]
public enum CardValue
{
    _2 = 4,
    _3 = 8,
    _4 = 16,
    _5 = 32,
    _6 = 64,
    _7 = 128,
    _8 = 256,
    _9 = 512,
    T = 1024,
    J = 2048,
    Q = 4096,
    K = 8192,
    A = 16384
}

public static class CardValueUtil
{
    public const string CharValues = "23456789TJQKA";
    
    public static char ToChar(this CardValue cardValue)
    {
        var cardValueInt = (int)cardValue;
        var bits = 0;
        while (cardValueInt >= 16)
        {
            bits += 4;
            cardValueInt >>= 4;
        }

        while (cardValueInt > 0)
        {
            bits++;
            cardValueInt >>= 1;
        }

        if (!bits.IsBetween(3, 15))
            throw new ArithmeticException($"Invalid card value: {cardValue}");
        return CharValues[bits - 3];
    }

    public static CardValue ToCardValue(this char @char)
    {
        var index = CharValues.IndexOf(@char);
        if (index == -1)
            throw new ArgumentException($"Invalid card char: {@char}");

        var intValue = 1 << (index + 2);
        return (CardValue)intValue;
    }
}