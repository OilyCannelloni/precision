namespace Precision.algorithm;

public static class CardExtensions
{
    public static string ToCardString(this int card)
    {
        if (card is < 0 or > 52)
            throw new ArgumentException($"{card} is not a valid card.");
        var suit = "SHDC"[card / 13];
        var value = "23456789TJQKA"[card % 13];
        return $"{value}{suit}";
    }

    public static int CardValue(this char card)
    {
        var value = "23456789TJQKA".IndexOf(char.ToUpper(card));
        if (value == -1)
            throw new ArgumentException($"{card} is not a valid card.");
        return value;
    }
}