namespace Precision.models;

public enum WebSocketEventType
{
    ConnectionSuccessful,
    CardClicked,
    NewGameRequest,
    NewGameCreated,
    PlayCardApproved,
    Error
}