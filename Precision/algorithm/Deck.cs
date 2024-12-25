namespace Precision.algorithm;

public class Deck
{
    private const int DeckLen = 52;
    private readonly int[] _deck = Enumerable.Range(0, DeckLen).ToArray();

    public Deck()
    {
        Random.Shared.Shuffle(_deck);
    }

    public IEnumerable<ArraySegment<int>> DealHands()
    {
        for (var i = 0; i < 52; i += 13)
            yield return new ArraySegment<int>(_deck, i, 13);
    }
}