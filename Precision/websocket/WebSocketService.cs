using Precision.deals;
using Precision.models;
using Swan.Formatters;

namespace Precision.websocket;

public class WebSocketService(DealService _dealService)
{
    public WebSocketEvent HandleEvent(WebSocketEvent @event)
    {
        return @event.Type switch
        {
            WebSocketEventType.CardClicked => HandleCardClicked(@event),
            WebSocketEventType.RandomDealRequest => HandleRandomDealRequest(@event),
            _ => throw new ArgumentOutOfRangeException($"Invalid event type: {@event.Type}")
        };
    }

    public WebSocketEvent HandleCardClicked(WebSocketEvent @event)
    {
        throw new NotImplementedException();
    }

    public WebSocketEvent HandleRandomDealRequest(WebSocketEvent @event)
    {
        var deal = _dealService.GetRandomDeal();

        return new WebSocketEvent
        {
            Type = WebSocketEventType.DealData,
            Data = Json.Serialize(deal)
        };
    }
}