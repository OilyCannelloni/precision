using EmbedIO.WebSockets;
using Precision.game.elements.cards;
using Precision.game.elements.deal;
using Precision.game.players;
using Precision.models.common;
using Precision.models.dto;

namespace Precision.game;


public class BotGame : Game
{
    private Table _table = new();

    public BotGame(string id, DealBox box) : base(id, box)
    {
        var botStrategyW = new BotStrategy(Position.West)
            .AddFilter(BotStrategyFilterAction.DoubleDummy)
            .AddFilter(BotStrategyFilterAction.CashWinners)
            .SetPickAction(BotStrategyPickAction.Lowest);

        var botStrategyE = new BotStrategy(Position.East)
            .AddFilter(BotStrategyFilterAction.Legal)
            .SetPickAction(BotStrategyPickAction.Lowest);
        
        _table.AddPlayer(new HumanPlayer(this, Position.South));
        _table.AddPlayer(new BotPlayer(this, Position.West, botStrategyW));
        _table.AddPlayer(new DummyPlayer(this, Position.North));
        _table.AddPlayer(new BotPlayer(this, Position.East, botStrategyE));
    }

    public override DealUpdateDto? PlayCard(Card card)
    {
        var dealUpdate = base.PlayCard(card);
        if (dealUpdate == null)
            return null;
        
        _table.DispatchDealUpdate(dealUpdate);
        return dealUpdate;
    }
}