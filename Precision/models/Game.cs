using System.Diagnostics.Contracts;

namespace Precision.models;

public class Game(DealBox box)
{
    public DealBox DealBox { get; set; } = box;
    public Deal CurrentDeal { get; set; } = box.Deal;
    public Bidding Bidding { get; set; } = new();
    public Position ActionPlayer { get; set; } = box.Dealer;
    public Bid? Contract { get; set; } = null;
    public int NsTricks = 0;
    public int EwTricks = 0;
}