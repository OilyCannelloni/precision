using Precision.algorithm;

namespace Precision.models;

public class Hand
{
    public string Spades { get; set; } = "";
    public string Hearts { get; set; } = "";
    public string Diamonds { get; set; } = "";
    public string Clubs { get; set; } = "";

    public IEnumerable<string> Suits()
    {
        yield return Spades;
        yield return Hearts;
        yield return Diamonds;
        yield return Clubs;
    }
    
    public override string ToString()
    {
        return string.Join(".", Suits());
    }


    public static Hand FromCards(ArraySegment<int> cards)
    {
        if (cards.Count != 13)
            throw new ArgumentException("Invalid number of cards in a hand.");
        
        // Array [0..4] of List<char>()
        var store = Enumerable.Range(0, 4).Select(_ => new List<char>()).ToArray();
        foreach (var card in cards)
        {
            if (card is < 0 or >= 52)
                throw new ArgumentException("Invalid card value.");
            store[card / 13].Add("23456789TJQKA"[card % 13]);
        }
        return new Hand
        {
            Spades = string.Join("", store[0].OrderBy(c => -c.CardValue())),
            Hearts = string.Join("", store[1].OrderBy(c => -c.CardValue())),
            Diamonds = string.Join("", store[2].OrderBy(c => -c.CardValue())),
            Clubs = string.Join("", store[3].OrderBy(c => -c.CardValue()))
        };
    }
}
