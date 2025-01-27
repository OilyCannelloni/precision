using Precision.game.elements.deal;
using Precision.models.common;
using Precision.models.dto;

namespace Precision.game;

public abstract class Player(Game game, Position position) : IEventObserver<DealUpdateDto>
{
    protected Game Game = game;
    protected Position Position = position;
    public abstract void OnNext(DealUpdateDto @new);
}