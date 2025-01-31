using Precision.game.dds;
using Precision.game.elements.cards;
using Precision.game.elements.deal;
using Precision.models.common;
using Precision.models.dto;

namespace Precision.game;

public class BotPlayerStrategy
{
    public bool DefaultPlayLow { get; set; } = false; // false = play random card
    public bool DefaultTakeTrickIfCan { get; set; } = true;
    public bool DefaultPlayDoubleDummy { get; set; } = true;
}


public class BotPlayer(Game game, Position position, BotPlayerStrategy strategy) : Player(game, position)
{
    private DdsService _ddsService = new();
    
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
        
        Console.WriteLine(string.Join(" ", hand.Holdings()));
        
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

    private Card PickCardDdRandom()
    {
        var cards = _ddsService.SolveCurrentGameState(Game).ToArray();
        Console.WriteLine($"Bot {Position}: {string.Join(", ", cards.AsReadOnly())}");
        var pickedCard = cards[Random.Shared.Next(cards.Length)];
        return pickedCard;
    }
    
    public override void OnDealUpdate(DealUpdateDto dealUpdateDto)
    {
        if (dealUpdateDto.ActionPlayer != Position)
            return;
        
        if (Game.CurrentDealState[Position].IsEmpty())
            return;
        
        Console.WriteLine($"Bot {Position}: Picking card...");
        // var pickedCard = PickCardByDefaultStrategy(Game.CurrentDealState[Position], Game.CurrentTrick);
        var pickedCard = PickCardDdRandom();
        Console.WriteLine($"Bot {Position}: Picked {pickedCard}");

        if (game.CurrentTrick.IsEmpty())
            PlayCardDelayAsync(pickedCard);
        else
            Game.PlayCard(pickedCard);
    }

    private async Task PlayCardDelayAsync(Card card)
    {
        var task = Task.Run(async () =>
        {
            await Task.Delay(1000);
            return card;
        });
        await task.WaitAsync(TimeSpan.FromMilliseconds(2000));
        Game.PlayCard(task.Result);
    }
}