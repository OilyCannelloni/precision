namespace Precision.models;

public class DealBox(int number, Deal deal)
{
    private static readonly int[] VulOrder = [0, 1, 2, 3, 1, 2, 3, 0, 2, 3, 0, 1, 3, 0, 1, 2];
    private static readonly Position[] PosOrder = [Position.North, Position.East, Position.South, Position.West];

    public int Number { get; set; } = number;
    public Vulnerability Vulnerability { get; set; } = GetVulFromNumber(number);
    public Position Dealer { get; set; } = GetDealerFromNumber(number);
    public Deal Deal { get; set; } = deal;

    private static Vulnerability GetVulFromNumber(int number)
    {
        var n = PosOrder[(number - 1) % 16];
        return (Vulnerability)n;
    }

    private static Position GetDealerFromNumber(int number)
    {
        return PosOrder[(number - 1) % 4];
    }
}