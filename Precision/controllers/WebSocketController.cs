using EmbedIO.WebSockets;
using Precision.models.socket;
using Precision.websocket;
using Swan.Formatters;

namespace Precision.controllers;

public class WebSocketController : WebSocketModule
{
    private readonly WebSocketService _webSocketService;

    public WebSocketController(string urlPath, bool enableConnectionWatchdog)
        : base(urlPath, enableConnectionWatchdog)
    {
        AddProtocol("json");
        _webSocketService = new WebSocketService(this);
    }

    protected override async Task OnMessageReceivedAsync(IWebSocketContext context, byte[] buffer,
        IWebSocketReceiveResult result)
    {
        var text = Encoding.GetString(buffer);
        // Console.WriteLine($"RX: {text}");
        var evt = Json.Deserialize<WebSocketEvent>(text);
        var serverEvt = _webSocketService.HandleEvent(context, evt);
        
        if (serverEvt != null)
            await SendEvent(context, serverEvt);
    }

    protected override async Task OnClientConnectedAsync(IWebSocketContext context)
    {
        Console.WriteLine("Client connected");
        await SendEvent(context, new WebSocketEvent
        {
            Type = WebSocketEventType.ConnectionSuccessful,
            Data = ""
        });
    }

    protected override Task OnClientDisconnectedAsync(IWebSocketContext context)
    {
        Console.WriteLine("Client disconnected");
        return Task.CompletedTask;
    }

    public async Task SendEvent(IWebSocketContext ctx, WebSocketEvent evt)
    {
        var str = Json.Serialize(evt);
        Console.WriteLine($"TX: {evt.Data}");
        await SendAsync(ctx, str);
    }
}