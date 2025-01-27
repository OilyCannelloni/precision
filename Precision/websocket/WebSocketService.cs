using System.Runtime.Serialization;
using System.Text.Json;
using Precision.deals;
using Precision.game;
using Precision.models;
using Precision.models.dto;

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

    private WebSocketEvent HandleCardClicked(WebSocketEvent @event)
    {
        CardClickedDto ccDto = JsonSerializer.Deserialize<CardClickedDto>(@event.Data) 
                               ?? throw new SerializationException("Invalid payload for CardClicked event");
        
        var game = gameService.GetGame(ccDto.GameId);
        var dealUpdate = game.PlayCard(new Card(ccDto.Card));
        if (dealUpdate == null)
        {
            return new WebSocketEvent
            {
                Type = WebSocketEventType.Error,
                Data = $"Card {ccDto.Card} cannot be played."
            };
        }

        return new WebSocketEvent
        {
            Type = WebSocketEventType.PlayCardApproved,
            Data = JsonSerializer.Serialize(dealUpdate)
        };
    }

    private WebSocketEvent HandleNewGameRequest(WebSocketEvent @event)
    {
        var deal = dealService.GetRandomDeal();
        var dealBox = new DealBox(2, deal);
        var gameId = gameService.CreateGame(dealBox);
        var str = JsonSerializer.Serialize(new NewGameDto
            { GameId = gameId, DealBox = dealBox, CurrentTrick = new Trick(dealBox.Dealer)});
        
        return new WebSocketEvent
        {
            Type = WebSocketEventType.NewGameCreated,
            Data = str
        };
    }
}