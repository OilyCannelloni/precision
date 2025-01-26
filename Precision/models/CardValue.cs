using Swan;

namespace Precision.models;

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
    public static int GetChar(int cardValue)
    {
        int bits = 0;
        while (cardValue >= 16) {
            bits += 4;
            cardValue >>= 4;
        }
        while (cardValue > 0) {
            bits++;
            cardValue >>= 1;
        }

        if (!bits.IsBetween(2, 14))
            throw new ArithmeticException($"Invalid card value: {cardValue}");
        return Card.Values[bits - 2];
    }
}
