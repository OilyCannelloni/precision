using System.Text.Json.Serialization;

namespace Precision.game.elements.cards;

public class Card
{
    public Card(string str)
    {
        Suit = str[1].ToSuit();
        Value = char.ToUpper(str[0]);
        IntValue = str[0].ToCardValue();
    }

    public Card(Suit suit, CardValue cardValue)
    {
        Suit = suit;
        IntValue = cardValue;
        Value = cardValue.ToChar();
    }

    public Suit Suit { get; set; }

    public char Value { get; set; }

    [JsonIgnore] public CardValue IntValue { get; set; }

    public override string ToString()
    {
        return $"{Value}{Suit.ToChar()}";
    }
}