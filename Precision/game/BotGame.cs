using EmbedIO.WebSockets;
using Precision.game.elements.cards;
using Precision.game.elements.deal;
using Precision.game.players;
using Precision.models.common;
using Precision.models.dto;

namespace Precision.game;


public class BotGame : Game
{
    private List<IEventObserver<DealUpdateDto>> _stateChangeObservers = new();

    public BotGame(DealBox box, IWebSocketContext webSocketContext) : base(box)
    {
        _stateChangeObservers.Add(new BotPlayer(this, Position.West, new BotPlayerStrategy()));
        _stateChangeObservers.Add(new DummyPlayer(this, Position.North));
        _stateChangeObservers.Add(new BotPlayer(this, Position.East, new BotPlayerStrategy()));
        _stateChangeObservers.Add(new HumanPlayer(this, Position.South, webSocketContext));
    }

    public new DealUpdateDto? PlayCard(Card card)
    {
        var dealUpdate = base.PlayCard(card);
        if (dealUpdate == null)
            return null;
        
        foreach (var observer in _stateChangeObservers)
        {
            observer.OnNext(dealUpdate);
        }

        return dealUpdate;
    }
}