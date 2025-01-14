namespace Precision.models;

public class Bid(string str)
{
    public BidType Type { get; set; }
    public int Level { get; set; }
    public Suit? Suit { get; set; }
    public string Alert = string.Empty;
}