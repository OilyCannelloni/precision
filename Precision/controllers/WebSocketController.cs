using EmbedIO.WebSockets;
using Precision.models;
using Swan.Formatters;

namespace Precision.controllers;

public class WebSocketController : WebSocketModule
{
    public WebSocketController(string urlPath, bool enableConnectionWatchdog)
        : base(urlPath, enableConnectionWatchdog)
    {
        AddProtocol("json");
    }
    
    protected override async Task OnMessageReceivedAsync(IWebSocketContext context, byte[] buffer, IWebSocketReceiveResult result)
    {
        var text = Encoding.GetString(buffer);
        var evt = Json.Deserialize<WebSocketEvent>(text);
        
    }

    protected override async Task OnClientConnectedAsync(IWebSocketContext context)
    {
        
    }

    protected override Task OnClientDisconnectedAsync(IWebSocketContext context)
    {
        return base.OnClientDisconnectedAsync(context);
    }
}