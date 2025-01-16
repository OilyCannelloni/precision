import {IDealBox} from "./deal"

export enum SocketEventType {
    ConnectionSuccessful,
    CardClicked,
    NewGameRequest,
    NewGameCreated,
    DealData
}

export interface SocketEvent {
    Type: SocketEventType;
    Data: string
}

export interface INewGameDTO {
    GameId: string,
    DealBox: IDealBox
}