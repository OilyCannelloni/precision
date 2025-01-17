"use client";

import { useEffect, useCallback, useContext } from 'react';
import useWebSocket from 'react-use-websocket'
import game_elements from "@/components/game_elements.module.scss"
import {INewGameDTO, IDealUpdateDTO, SocketEvent, SocketEventType} from '@/models/socket';
import {Deal, Position, CardSuit, Card} from "@/models/deal"
import {HookContext} from "@/common/hooks"


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
                break;
            case SocketEventType.DealUpdate:
                const duDTO = JSON.parse(event.Data) as IDealUpdateDTO
                const pos = duDTO.ChangedPosition
                const card = new Card(duDTO.ChangedCard)
                console.log("dooopa")
                console.log(hooks)
                const oldHolding = hooks[pos][card.Suit].Value
                const newHolding = oldHolding.replace(card.Value, "");
                console.log(newHolding)
                hooks[pos][card.Suit].SetValue(newHolding)
                break;
            default:
                throw new TypeError(`Invalid event type: ${event.Type}`)
        }
        
    }, [lastMessage])
    
    
    return <div className={game_elements.redealButton}>
        <button onClick={createGame}>New Game</button>
    </div>
};
