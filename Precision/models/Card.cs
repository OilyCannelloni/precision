using System.Text.Json.Serialization;
using Precision.algorithm.json;

namespace Precision.models;


public class Card
{
    public const string Values = "23456789TJQKA";
    
    public Suit Suit { get; set; }
    public char Value { get; set; }
    
    [JsonIgnore]
    public int IntValue { get; set; }

    public Card(string str)
    {
        Suit = SuitExtensions.FromChar(char.ToLower(str[1]));
        IntValue = Values.IndexOf(char.ToUpper(str[0]));
        Value = char.ToUpper(str[0]);
    }

    public char Char()
    {
        return Values[IntValue];
    }

    public override string ToString()
    {
        return $"{Values[IntValue]}{Suit.ToChar()}";
    }
}