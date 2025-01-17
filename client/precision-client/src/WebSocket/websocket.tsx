"use client";

import { useEffect, useCallback, useContext } from 'react';
import useWebSocket from 'react-use-websocket'
import game_elements from "@/components/game_elements.module.scss"
import {INewGameDTO, IPlayCardApprovedDTO, ICardClickedDTO, SocketEvent, SocketEventType} from '@/models/socket';
import {Deal, Position, CardSuit, Card, CardSuitHelper} from "@/models/deal"
import {HookContext, GlobalVarContext} from "@/common/hooks"


export function Connect() {
    const hooks = useContext(HookContext)
    const globals = useContext(GlobalVarContext)
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
    
    const setDealData = ((deal: Deal) => {
        for (const pos in Position) {
            for (const suit in CardSuit) {
                // @ts-ignore
                hooks[pos][suit].SetValue(deal[pos][suit])
            }
        }
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
                const deal = newGameDTO.DealBox.Deal
                setDealData(deal)
                globals.GameId = newGameDTO.GameId;
                break;
            case SocketEventType.PlayCardApproved:
                const pcaDTO = JSON.parse(event.Data) as IPlayCardApprovedDTO
                const pos = pcaDTO.ChangedPosition
                const card = new Card(pcaDTO.PlayedCard[0], CardSuitHelper.fromString(pcaDTO.PlayedCard[1]))
                console.log(card, hooks)
                const oldHolding = hooks[pos][card.Suit].Value
                const newHolding = oldHolding.replace(card.Value, "");
                hooks[pos][card.Suit].SetValue(newHolding)
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
                GameId: globals.GameId,
                Card: card.cardStr()
            }
            const socketEvent: SocketEvent = {
                Type: SocketEventType.CardClicked,
                Data: JSON.stringify(dto)
            }
            sendMessage(JSON.stringify(socketEvent))
        }
        
        window.addEventListener("cardClicked", handleCardClicked)
        
        return () => {
            window.removeEventListener("cardClicked", handleCardClicked)
        }
    }, []);
    
    
    return <div className={game_elements.redealButton}>
        <button onClick={createGame}>New Game</button>
    </div>
};
