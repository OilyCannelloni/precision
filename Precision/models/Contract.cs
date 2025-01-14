namespace Precision.models;

public enum ContractType
{
    Default, Doubled, Redoubled, Pass
}

public class Contract
{
    public int Level { get; set; }
    public Suit Suit { get; set; }
    public ContractType Type { get; set; }
}