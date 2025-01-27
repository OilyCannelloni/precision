import {Card, IDealBox, Position, Trick} from "./deal"

export enum SocketEventType {
    ConnectionSuccessful,
    CardClicked,
    NewGameRequest,
    NewGameCreated,
    PlayCardApproved,
    Error
}

export interface SocketEvent {
    Type: SocketEventType;
    Data: string
}

export interface INewGameDTO {
    GameId: string,
    DealBox: IDealBox
    CurrentTrick: Trick
}

export interface ICardClickedDTO {
    GameId: string,
    Card: string
}

export interface IPlayCardApprovedDTO {
    ChangedPosition: Position,
    PlayedCard: Card,
    CurrentTrick: Trick,
    ActionPlayer: Position
}