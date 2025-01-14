using EmbedIO;
using EmbedIO.WebApi;
using Precision.controllers;
using Swan.Logging;


Console.WriteLine("Hello, World!");

const string url = "http://localhost:9696/";

using var server = new WebServer(s => s
    .WithUrlPrefix(url)
    .WithMode(HttpListenerMode.EmbedIO)
);

server.WithWebApi("/api", x => x.WithController<DefaultController>());
server.WithModule(new WebSocketController("/ws", true));
server.WithStaticFolder("/", @"C:\Users\barte\RiderProjects\Precision\Precision\htmlroot", true);
server.StateChanged += (s, e) => $"WebServer New State - {e.NewState}".Info();

server.RunAsync();

Console.ReadKey(true);