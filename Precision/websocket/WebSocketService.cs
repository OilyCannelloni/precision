using Precision.deals;
using Precision.game;
using Precision.models;
using Precision.models.dto;
using Swan.Formatters;

namespace Precision.websocket;

public class WebSocketService(DealService dealService, GameService gameService)
{
    public WebSocketEvent HandleEvent(WebSocketEvent @event)
    {
        return @event.Type switch
        {
            WebSocketEventType.CardClicked => HandleCardClicked(@event),
            WebSocketEventType.NewGameRequest => HandleNewGameRequest(@event),
            _ => throw new ArgumentOutOfRangeException($"Invalid event type: {@event.Type}")
        };
    }

    public WebSocketEvent HandleCardClicked(WebSocketEvent @event)
    {
        throw new NotImplementedException();
    }

    public WebSocketEvent HandleNewGameRequest(WebSocketEvent @event)
    {
        var deal = dealService.GetRandomDeal();
        var dealBox = new DealBox(2, deal);
        var gameId = gameService.CreateGame(dealBox);
        var str = Json.Serialize(new NewGameDto { GameId = gameId, Box = dealBox });
        
        Console.WriteLine(str);
        
        return new WebSocketEvent
        {
            Type = WebSocketEventType.NewGameCreated,
            Data = str
        };
    }
}