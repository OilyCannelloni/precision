using Precision.models.dto;

namespace Precision.models;

public class Game(DealBox box)
{
    public DealBox DealBox { get; set; } = box;
    public Deal CurrentDealState { get; set; } = box.Deal;
    public List<Card> CurrentTrick = [];
    public Bidding Bidding { get; set; } = new();
    public Position ActionPlayer { get; set; } = box.Dealer;
    public Bid? Contract { get; set; } = null!;
    public int NsTricks = 0;
    public int EwTricks = 0;

    public DealUpdateDto? PlayCard(Card card)
    {
        if (!_canPlayCard(card)) 
            return null;
        
        var requestPlayer = ActionPlayer;
        CurrentDealState.RemoveCard(requestPlayer, card);
        
        CurrentTrick.Add(card);
        if (CurrentTrick.Count < 4)
        {
            ActionPlayer = ActionPlayer.Next();
            return new DealUpdateDto
            {
                ChangedPosition = requestPlayer,
                ChangedCard = card.ToString(),
                CurrentDealMiddle = CurrentTrick.Select(c => c.ToString()).ToList(),
            };
        }

        ActionPlayer = _resolveCurrentTrick(ActionPlayer);
        return new DealUpdateDto
        {
            ChangedPosition = requestPlayer,
            ChangedCard = card.ToString(),
            CurrentDealMiddle = []
        };
    }
    private bool _canPlayCard(Card card)
    {
        if (!CurrentDealState[ActionPlayer].ContainsCard(card))
            return false;
        if (CurrentTrick.Count == 0)
            return true;

        var leadCard = CurrentTrick[0];
        if (card.Suit == leadCard.Suit)
            return true;
        if (CurrentDealState[ActionPlayer][card.Suit].Length == 0)
            return true;
        return false;
    }

    private Position _resolveCurrentTrick(Position dealer)
    {
        if (Contract == null || CurrentTrick.Count != 4)
            throw new ArgumentException("Resolved trick is not complete or there is no established contract.");

        var trumpSuit = Contract!.Suit;
        var leadSuit = CurrentTrick[0].Suit;
        var bestCardIndex = 0;
        var bestCardValue = 0;
        var i = 0;
        foreach (var card in CurrentTrick)
        {
            if (card.Suit == leadSuit && card.Value > bestCardValue)
            {
                bestCardValue = card.Value;
                bestCardIndex = i++;
                continue;
            }

            if (card.Suit == trumpSuit)
            {
                bestCardValue = card.Value;
                bestCardIndex = i++;
                leadSuit = trumpSuit;
            }
        }

        return dealer.ShiftBy(bestCardIndex);
    }
}







