using Precision.models.dto;

namespace Precision.models;

public class Game(DealBox box)
{
    public DealBox DealBox { get; set; } = box;
    public Deal CurrentDealState { get; set; } = box.Deal;
    public Trick CurrentTrick = new(box.Dealer);
    public Bidding Bidding { get; set; } = new();
    public Position ActionPlayer { get; set; } = box.Dealer;
    public Bid? Contract { get; set; } = null!;
    public int NsTricks = 0;
    public int EwTricks = 0;

    public PlayCardApprovedDto? PlayCard(Card card)
    {
        if (!_canPlayCard(card)) 
            return null;
        
        var requestPlayer = ActionPlayer;
        CurrentDealState.RemoveCard(requestPlayer, card);
        
        CurrentTrick.AddCard(card);
        if (CurrentTrick.IsComplete())
        {
            ActionPlayer = CurrentTrick.ResolveWinner(
                Contract?.Suit ?? throw new NullReferenceException("Cannot resolve a trick without a contract"));
        }
        else
        {
            ActionPlayer = ActionPlayer.Next();
        }
        
        return new PlayCardApprovedDto
        {
            ChangedPosition = requestPlayer,
            PlayedCard = card.ToString(),
            CurrentTrick = CurrentTrick,
            ActionPlayer = ActionPlayer
        };
    }
    private bool _canPlayCard(Card card)
    {
        if (!CurrentDealState[ActionPlayer].ContainsCard(card))
            return false;

        var leadCard = CurrentTrick.LeadCard();
        if (leadCard == null)
            return true;
        
        if (card.Suit == leadCard.Suit)
            return true;
        if (CurrentDealState[ActionPlayer][card.Suit].Length == 0)
            return true;
        return false;
    }
}







