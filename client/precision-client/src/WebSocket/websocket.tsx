"use client";

import { useEffect, useCallback } from 'react';
import useWebSocket, {ReadyState} from 'react-use-websocket'
import game_elements from "@/components/game_elements.module.scss"
import { DealModel } from '@/components/deal';
import { IDeal, IDealBox } from '@/models/deal';
import {INewGameDTO, SocketEvent } from '@/models/socket';


export function Connect({url, setDealData} : {url: string, setDealData: (deal: DealModel) => void}) {
    const { sendMessage, lastMessage, readyState } = useWebSocket("ws://localhost:9696/ws", {protocols: "json"})
    
    const createGame = useCallback(() => {
        const event = {
            "Type": "NewGameRequest",
            "Data": {}
        }
        const str = JSON.stringify(event);
        console.log("TX: " + str)
        sendMessage(str)
    }, [])

    useEffect(() => {
        if (lastMessage == null) return;
        console.log("RX: " + lastMessage)
        const event = JSON.parse(lastMessage.data) as SocketEvent;
        
        if (event.Type == 3) {
            const newGameDTO = JSON.parse(event.Data) as INewGameDTO
            const deal = newGameDTO.DealBox.Deal
            setDealData(deal)
        }
        
    }, [lastMessage])
    
    
    return <div className={game_elements.redealButton}>
        <button onClick={createGame}>New Game</button>
    </div>
};
