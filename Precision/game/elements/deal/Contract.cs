using Precision.game.elements.cards;

namespace Precision.game.elements.deal;

public enum ContractType
{
    Default,
    Doubled,
    Redoubled,
    Pass
}

public class Contract
{
    public int Level { get; set; }
    public Suit Suit { get; set; }
    public ContractType Type { get; set; }
}