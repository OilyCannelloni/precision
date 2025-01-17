namespace Precision.models;

public class Card
{
    public const string Values = "23456789TJQKA";
    
    public Suit Suit { get; set; }
    public int Value { get; set; }

    public Card(string str)
    {
        str = str.ToLower();
        Suit = SuitExtensions.FromChar(str[1]);
        Value = Values.IndexOf(str[0]);
    }

    public char Char()
    {
        return Values[Value];
    }

    public override string ToString()
    {
        return $"{Values[Value]}{Suit.ToChar()}";
    }
}