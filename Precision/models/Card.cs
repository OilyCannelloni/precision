using System.Text.Json.Serialization;

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
        IntValue = 1 << (Values.IndexOf(char.ToUpper(str[0])) + 2);
        Value = char.ToUpper(str[0]);
    }

    public override string ToString()
    {
        return $"{Value}{Suit.ToChar()}";
    }
}