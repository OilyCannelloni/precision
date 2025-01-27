namespace Precision.models.socket;

public enum WebSocketEventType
{
    ConnectionSuccessful,
    CardClicked,
    NewGameRequest,
    NewGameCreated,
    PlayCardApproved,
    Error
}