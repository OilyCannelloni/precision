using EmbedIO.WebSockets;
using Precision.game.elements.cards;
using Precision.game.elements.deal;
using Precision.models.dto;

namespace Precision.game;




public class GameService
{
    private class GameInfo
    {
        public required Game Game { get; set; }
        public required IWebSocketContext WebSocketContext { get; set; }
    }
    
    
    private readonly Dictionary<string, GameInfo> _games = new();

    public string CreateGame(IWebSocketContext webSocketContext, DealBox box)
    {
        var id = Guid.NewGuid().ToString();
        _games[id] = new GameInfo
        {
            Game = new Game(id, box),
            WebSocketContext = webSocketContext
        };
        return id;
    }
    
    public string CreateBotGame(IWebSocketContext webSocketContext, DealBox box)
    {
        var id = Guid.NewGuid().ToString();
        _games[id] = new GameInfo
        {
            Game = new BotGame(id, box),
            WebSocketContext = webSocketContext
        };
        return id;
    }

    public Game GetGame(string id)
    {
        return _games[id].Game;
    }

    public IWebSocketContext GetSocketById(string id)
    {
        return _games[id].WebSocketContext;
    }

    public DealUpdateDto? OnCardPlayRequest(string gameId, Card card)
    {
        var game = GetGame(gameId);
        return game.PlayCard(card);
    }
}