using Precision.game.elements.cards;

namespace Precision.game.elements.deal;

public enum ContractType
{
    Default,
    Doubled,
    Redoubled,
    Pass
}

public class Contract(string s)
{
    public int Level { get; set; } = s[0] - '0';
    public Suit Suit { get; set; } = s[1].ToSuit();
    public ContractType Type { get; set; } = ContractType.Default;
}