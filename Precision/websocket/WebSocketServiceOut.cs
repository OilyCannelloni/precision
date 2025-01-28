using System.Text.Json;
using Precision.game.players;
using Precision.models.dto;
using Precision.models.socket;

namespace Precision.websocket;

public partial class WebSocketService
{
    private void CreateEventHandlers()
    {
        HumanPlayer.DealUpdateReady += SendDealUpdate;
    }
    
    private void SendDealUpdate(object? o, DealUpdateDto dealUpdate)
    {
        var socketContext = _gameService.GetSocketById(dealUpdate.GameId);
        _ = _controller.SendEvent(socketContext, new WebSocketEvent
        {
            Type = WebSocketEventType.PlayCardApproved,
            Data = JsonSerializer.Serialize(dealUpdate)
        });
    }
}