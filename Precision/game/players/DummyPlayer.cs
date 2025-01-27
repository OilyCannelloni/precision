using Precision.game.elements.deal;
using Precision.models.common;
using Precision.models.dto;

namespace Precision.game;

public class DummyPlayer(Game game, Position position) : Player(game, position)
{
    public override void OnNext(DealUpdateDto @new)
    {
        
    }
}