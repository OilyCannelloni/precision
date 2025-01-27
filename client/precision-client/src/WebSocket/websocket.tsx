"use client";

import { useEffect, useCallback, useContext } from 'react';
import useWebSocket from 'react-use-websocket'
import game_elements from "@/components/game_elements.module.scss"
import {INewGameDTO, IPlayCardApprovedDTO, ICardClickedDTO, SocketEvent, SocketEventType} from '@/models/socket';
import {Deal, Position, CardSuit, Card, CardSuitHelper, Trick} from "@/models/deal"
import {HookContext, GlobalVarContext} from "@/common/hooks"

var gameId = "xxx";

export function Connect() {
    const hooks = useContext(HookContext)
    const address = "ws://localhost:9696/ws"
    const { sendMessage, lastMessage, readyState } = useWebSocket(address, {protocols: "json"})
    
    const createGame = useCallback(() => {
        const event = {
            "Type": "NewGameRequest",
            "Data": {}
        }
        const str = JSON.stringify(event);
        console.log("TX: " + str)
        sendMessage(str)
    }, [])
    
    const setDealData = ((gameState: INewGameDTO) => {
        for (const pos in Position) {
            for (const suit in CardSuit) {
                // @ts-ignore
                hooks[pos][suit].SetValue(gameState.DealBox.Deal[pos][suit])
            }
        }
        hooks.DealMiddle.SetValue(gameState.CurrentTrick)
    })

    useEffect(() => {
        if (lastMessage == null) return;
        console.log("RX: " + lastMessage.data)
        const event = JSON.parse(lastMessage.data) as SocketEvent;
        
        switch (event.Type) {
            case SocketEventType.ConnectionSuccessful:
                console.log(`WebSocket connected to ${address}`)
                break;
            case SocketEventType.NewGameCreated:
                const newGameDTO = JSON.parse(event.Data) as INewGameDTO
                setDealData(newGameDTO)
                gameId = newGameDTO.GameId;
                console.log("New game: " + newGameDTO.GameId)
                break;
            case SocketEventType.PlayCardApproved:
                const pcaDTO = JSON.parse(event.Data) as IPlayCardApprovedDTO
                const pos = pcaDTO.ChangedPosition
                const card = Card.fromPartial(pcaDTO.PlayedCard)
                const oldHolding = hooks[pos][card.Suit].Value
                const newHolding = oldHolding.replace(card.Value, "");
                hooks[pos][card.Suit].SetValue(newHolding)
                
                const trick = Object.assign(new Trick(), pcaDTO.CurrentTrick)
                trick[Position.West] = pcaDTO.CurrentTrick.West ? Card.fromPartial(pcaDTO.CurrentTrick.West) : null
                trick[Position.North] = pcaDTO.CurrentTrick.North ? Card.fromPartial(pcaDTO.CurrentTrick.North) : null
                trick[Position.East] = pcaDTO.CurrentTrick.East ? Card.fromPartial(pcaDTO.CurrentTrick.East) : null
                trick[Position.South] = pcaDTO.CurrentTrick.South ? Card.fromPartial(pcaDTO.CurrentTrick.South) : null
                
                hooks.DealMiddle.SetValue(trick)
                break;
            case SocketEventType.Error:
                const message = event.Data;
                console.log(message)
                break;
            default:
                throw new TypeError(`Invalid event type: ${event.Type}`)
        }
        
    }, [lastMessage])

    useEffect(() => {
        function handleCardClicked(event: Event) {
            const card = (event as CustomEvent).detail as Card
            const dto: ICardClickedDTO = {
                GameId: gameId,
                Card: card.cardStr()
            }
            const socketEvent: SocketEvent = {
                Type: SocketEventType.CardClicked,
                Data: JSON.stringify(dto)
            }
            console.log(gameId)
            sendMessage(JSON.stringify(socketEvent))
        }
        
        window.addEventListener("cardClicked", handleCardClicked)
        
        return () => {
            window.removeEventListener("cardClicked", handleCardClicked)
        }
    }, [gameId]);
    
    
    return <div className={game_elements.redealButton}>
        <button onClick={createGame}>New Game</button>
    </div>
};
