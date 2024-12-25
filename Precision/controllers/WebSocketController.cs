using EmbedIO.WebSockets;

namespace Precision.controllers;

public class WebSocketController(string urlPath, bool enableConnectionWatchdog)
    : WebSocketModule(urlPath, enableConnectionWatchdog)
{
    protected override Task OnMessageReceivedAsync(IWebSocketContext context, byte[] buffer, IWebSocketReceiveResult result)
    {
        throw new NotImplementedException();
    }
}