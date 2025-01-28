using Precision.game.elements.cards;
using Precision.game.elements.deal;
using Precision.models.common;
using Precision.models.dto;

namespace Precision.game;

public class BotPlayerStrategy
{
    public bool DefaultPlayLow { get; set; } = false; // false = play random card
    public bool DefaultTakeTrickIfCan { get; set; } = true;
}


public class BotPlayer(Game game, Position position, BotPlayerStrategy strategy) : Player(game, position)
{
    private Card PickCardByDefaultStrategy(Hand hand, Trick currentTrick)
    {
        if (currentTrick.IsEmpty())
            return PickCardByDefaultStrategyFromAll(hand);

        var trickSuit = currentTrick.LeadCard()?.Suit ?? throw new ArgumentNullException(nameof(currentTrick));
        if (hand[trickSuit].IsEmpty())
            return PickCardByDefaultStrategyFromAll(hand);
        return PickCardByDefaultStrategyFromSuit(hand, trickSuit);
    }

    private Card PickCardByDefaultStrategyFromAll(Hand hand)
    {
        if (strategy.DefaultPlayLow)
        {
            var suit = SuitExtensions.GetRandomSuit();
            return hand.PopLowestFrom(suit);
        }
        
        Console.WriteLine(string.Join(" ", hand.Suits()));
        
        var cards = hand.AsCards().ToArray();
        var pickedCard = cards[Random.Shared.Next(cards.Length)];
        return pickedCard;
    }

    private Card PickCardByDefaultStrategyFromSuit(Hand hand, Suit suit)
    {
        if (strategy.DefaultPlayLow)
        {
            return hand.PopLowestFrom(suit);
        }
        
        Console.WriteLine(string.Join(" ", hand[suit]), suit);

        var cards = hand[suit].AsCardValues().Select(cv => new Card(suit, cv)).ToArray();
        var pickedCard = cards[Random.Shared.Next(cards.Length)];
        return pickedCard;
    }

    public override void OnDealUpdate(DealUpdateDto dealUpdateDto)
    {
        if (dealUpdateDto.ActionPlayer != Position)
            return;
        
        Console.WriteLine($"Bot {Position}: Picking card...");
        var pickedCard = PickCardByDefaultStrategy(Game.CurrentDealState[Position], Game.CurrentTrick);
        Console.WriteLine($"Bot {Position}: Picked {pickedCard}");
        Game.PlayCard(pickedCard);
    }
}