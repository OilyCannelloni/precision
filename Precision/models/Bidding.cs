namespace Precision.models;

public class Bidding
{
    public List<Bid> Bids { get; set; } = [];

    private bool IsNextBidLegal(Bid bid)
    {
        var last3 = Bids.Slice(Bids.Count - 3, 3);
        if (last3.TrueForAll(b => b.Type == BidType.Pass))
            return false;
        
        switch (bid.Type)
        {
            case BidType.Bid:
                var last = Bids.Last();
                return bid.Level > last.Level || bid.Level == last.Level && bid.Suit > last.Suit;
            case BidType.Pass:
                return true;
            case BidType.Double:
                if (last3[2].Type == BidType.Bid)
                    return true;
                if (last3[2].Type is BidType.Double or BidType.Redouble)
                    return false;
                if (last3[1].Type != BidType.Pass)
                    return false;
                if (last3[0].Type == BidType.Bid)
                    return true;
                return false;
            case BidType.Redouble:
                if (last3[2].Type == BidType.Double)
                    return true;
                if (last3[2].Type == BidType.Pass && last3[1].Type == BidType.Pass && last3[0].Type == BidType.Double)
                    return true;
                return false;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public bool Add(Bid bid)
    {
        if (!IsNextBidLegal(bid))
            return false;
        Bids.Add(bid);
        return true;
    }
}