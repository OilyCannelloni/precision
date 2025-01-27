using System.Runtime.Serialization;
using System.Text.Json;
using Precision.controllers;
using Precision.deals;
using Precision.game;
using Precision.game.elements.cards;
using Precision.game.elements.deal;
using Precision.models.dto;
using Precision.models.socket;

namespace Precision.websocket;

public partial class WebSocketService(WebSocketController controller)
{
    private readonly DealService _dealService = new();
    private readonly GameService _gameService = new();
    private readonly WebSocketController _controller = controller;

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
        var ccDto = JsonSerializer.Deserialize<CardClickedDto>(@event.Data)
                    ?? throw new SerializationException("Invalid payload for CardClicked event");
        
        var dealUpdate = _gameService.OnCardPlayRequest(ccDto.GameId, new Card(ccDto.Card));
        if (dealUpdate == null)
            return new WebSocketEvent
            {
                Type = WebSocketEventType.Error,
                Data = $"Card {ccDto.Card} cannot be played."
            };

        return new WebSocketEvent
        {
            Type = WebSocketEventType.PlayCardApproved,
            Data = JsonSerializer.Serialize(dealUpdate)
        };
    }

    private WebSocketEvent HandleNewGameRequest(WebSocketEvent @event)
    {
        var deal = _dealService.GetRandomDeal();
        var dealBox = new DealBox(2, deal);
        var gameId = _gameService.CreateGame(dealBox);
        var str = JsonSerializer.Serialize(new NewGameDto
            { GameId = gameId, DealBox = dealBox, CurrentTrick = new Trick(dealBox.Dealer) });

        return new WebSocketEvent
        {
            Type = WebSocketEventType.NewGameCreated,
            Data = str
        };
    }
}