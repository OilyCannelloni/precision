using Precision.game.elements.deal;
using Precision.models.common;
using Precision.models.dto;

namespace Precision.game;

public abstract class Player(Game game, Position position)
{
    protected readonly Game Game = game;
    protected readonly Position Position = position;
    public abstract void OnDealUpdate(DealUpdateDto @new);
}