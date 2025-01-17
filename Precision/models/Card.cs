using System.Text.Json.Serialization;
using Precision.algorithm.json;

namespace Precision.models;

[JsonConverter(typeof(CardJsonConverter))]
public class Card
{
    public const string Values = "23456789TJQKA";
    
    public Suit Suit { get; set; }
    public int Value { get; set; }

    public Card(string str)
    {
        Suit = SuitExtensions.FromChar(char.ToLower(str[1]));
        Value = Values.IndexOf(char.ToUpper(str[0]));
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