using System.Text.Json.Serialization;

namespace Precision.game.elements.cards;

public class Card(string str)
{
    public const string Values = "23456789TJQKA";

    public Suit Suit { get; set; } = SuitExtensions.FromChar(char.ToLower(str[1]));

    public char Value { get; set; } = char.ToUpper(str[0]);

    [JsonIgnore] public int IntValue { get; set; } = 1 << (Values.IndexOf(char.ToUpper(str[0])) + 2);

    public override string ToString()
    {
        return $"{Value}{Suit.ToChar()}";
    }
}