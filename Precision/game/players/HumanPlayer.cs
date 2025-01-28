using Precision.game.elements.deal;
using Precision.models.dto;

namespace Precision.game.players;

public class HumanPlayer(Game game, Position position) : Player(game, position)
{
    public static event EventHandler<DealUpdateDto> DealUpdateReady;
    
    public override void OnDealUpdate(DealUpdateDto dealUpdateDto)
    {
        Console.WriteLine($"Human {Position}: Dispatching DealUpdate {dealUpdateDto.PlayedCard}");
        DealUpdateReady.Invoke(this, dealUpdateDto);
    }
}