import {IDealBox, Position} from "./deal"

export enum SocketEventType {
    ConnectionSuccessful,
    CardClicked,
    NewGameRequest,
    NewGameCreated,
    DealUpdate,
    Error
}

export interface SocketEvent {
    Type: SocketEventType;
    Data: string
}

export interface INewGameDTO {
    GameId: string,
    DealBox: IDealBox
}

export interface ICardClickedDTO {
    GameId: string,
    Card: string
}

export interface IDealUpdateDTO {
    ChangedPosition: Position,
    ChangedCard: string,
    CurrentDealMiddle: string[]
}