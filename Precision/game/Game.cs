using Precision.game.elements.bidding;
using Precision.game.elements.cards;
using Precision.game.elements.deal;
using Precision.models.dto;

namespace Precision.game;

public class Game(string id, DealBox box)
{
    public string Id { get; set; } = id;
    public Trick CurrentTrick = new(box.Dealer);
    protected int EwTricks = 0;
    protected int NsTricks = 0;
    protected DealBox DealBox { get; set; } = box;
    public Deal CurrentDealState { get; set; } = box.Deal;
    protected Bidding Bidding { get; set; } = new();
    protected virtual Position ActionPlayer { get; set; } = box.Dealer;

    protected Bid? Contract { get; set; } = new()
    {
        Type = BidType.Bid,
        Suit = Suit.Spades,
        Level = 4
    };

    public virtual DealUpdateDto? PlayCard(Card card)
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
            CurrentTrick = new Trick(ActionPlayer);
        }
        else
        {
            ActionPlayer = ActionPlayer.Next();
        }
        
        Console.WriteLine($"PlayCard OK: {card}");

        return new DealUpdateDto
        {
            GameId = Id,
            ChangedPosition = requestPlayer,
            PlayedCard = card,
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
        if (CurrentDealState[ActionPlayer][leadCard.Suit].IsEmpty())
            return true;
        return false;
    }
}