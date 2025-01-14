namespace Precision.models;

public class WebSocketEvent
{
    public WebSocketEventType Type { get; set; }
    public string Data { get; set; } = null!;
}