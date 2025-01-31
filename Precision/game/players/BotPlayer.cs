using Precision.game.elements.cards;
using Precision.game.elements.deal;
using Precision.models.dto;

namespace Precision.game.players;

public class BotPlayer(Game game, Position position, BotStrategy strategy) : Player(game, position)
{
    
    public override void OnDealUpdate(DealUpdateDto dealUpdateDto)
    {
        if (dealUpdateDto.ActionPlayer != Position)
            return;
        
        if (Game.CurrentDealState[Position].IsEmpty())
            return;
        
        Console.WriteLine($"Bot {Position}: Picking card...");
        var pickedCard = strategy.PickCard(Game.CurrentDealState[Position].AsCards().ToList(), Game);
        Console.WriteLine($"Bot {Position}: Picked {pickedCard}");

        if (Game.CurrentTrick.IsEmpty())
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