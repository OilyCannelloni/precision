using EmbedIO.WebSockets;
using Precision.algorithm;
using Precision.deals;
using Precision.game;
using Precision.models;
using Precision.websocket;
using Swan.Formatters;

namespace Precision.controllers;

public class WebSocketController : WebSocketModule
{
    private readonly WebSocketService _webSocketService = 
        new(new DealService(new DealGenerator()), new GameService());
    
    public WebSocketController(string urlPath, bool enableConnectionWatchdog)
        : base(urlPath, enableConnectionWatchdog)
    {
        AddProtocol("json");
    }
    
    protected override async Task OnMessageReceivedAsync(IWebSocketContext context, byte[] buffer, IWebSocketReceiveResult result)
    {
        var text = Encoding.GetString(buffer);
        var evt = Json.Deserialize<WebSocketEvent>(text);
        var serverEvt = _webSocketService.HandleEvent(evt);
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

    private async Task SendEvent(IWebSocketContext ctx, WebSocketEvent evt)
    {
        await SendAsync(ctx, Json.Serialize(evt));
    }
}