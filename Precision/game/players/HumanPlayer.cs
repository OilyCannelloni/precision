using EmbedIO.WebSockets;
using Precision.game.elements.deal;
using Precision.models.dto;

namespace Precision.game.players;

public class HumanPlayer(Game game, Position position, IWebSocketContext webSocketContext) : Player(game, position)
{
    private IWebSocketContext _webSocketContext = webSocketContext;
    
    public override void OnNext(DealUpdateDto @new)
    {
        // _webSocketContext.WebSocket.SendAsync()
    }
}