using System.Runtime.Serialization;
using System.Text.Json;
using EmbedIO.WebSockets;
using Precision.controllers;
using Precision.deals;
using Precision.game;
using Precision.game.elements.cards;
using Precision.game.elements.deal;
using Precision.models.dto;
using Precision.models.socket;

namespace Precision.websocket;

public partial class WebSocketService
{
    private readonly DealService _dealService = new();
    private readonly GameService _gameService = new();
    private readonly WebSocketController _controller;

    public WebSocketService(WebSocketController controller)
    {
        _controller = controller;
        CreateEventHandlers();
    }


    public WebSocketEvent? HandleEvent(IWebSocketContext ctx, WebSocketEvent @event)
    {
        return @event.Type switch
        {
            WebSocketEventType.CardClicked => HandleCardClicked(ctx, @event),
            WebSocketEventType.NewGameRequest => HandleNewGameRequest(ctx, @event),
            _ => throw new ArgumentOutOfRangeException($"Invalid event type: {@event.Type}")
        };
    }

    private WebSocketEvent? HandleCardClicked(IWebSocketContext ctx, WebSocketEvent @event)
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

        return null;
    }

    private WebSocketEvent HandleNewGameRequest(IWebSocketContext ctx, WebSocketEvent @event)
    {
        var deal = _dealService.GetRandomDeal();
        var dealBox = new DealBox(3, deal);
        // var gameId = _gameService.CreateGame(ctx, dealBox);
        var gameId = _gameService.CreateBotGame(ctx, dealBox);
        var str = JsonSerializer.Serialize(new NewGameDto
            { GameId = gameId, DealBox = dealBox, CurrentTrick = new Trick(dealBox.Dealer) });

        return new WebSocketEvent
        {
            Type = WebSocketEventType.NewGameCreated,
            Data = str
        };
    }
}