"use client";

import { useEffect, useState } from 'react';
import game_elements from "@/components/game_elements.module.scss"
import { DealModel } from '@/components/deal';
import { IDeal } from '@/models/deal';

export function Connect({url, setDealData} : {url: string, setDealData: (deal: DealModel) => void}) {
    const [socket, setSocket] = useState(new WebSocket(url, "json"));
    const [socketMessage, setSocketMessage] = useState("")
    
    const socketService = useEffect(() => {
        socket.onopen = () => {
            console.log('Connected to server');
        };
        
        socket.onmessage = (msg) => {
            // console.log(msg.data)
            const event = JSON.parse(msg.data);
            // console.log(event.Data)
            if (event.Data)
                event.Data = JSON.parse(event.Data);
            console.log(event);
            
            if (event.Type == 3) {
                const deal = event.Data.Box.Deal as IDeal
                console.log(deal)
                setDealData(deal);
            }
            
        }
        
        socket.onerror = (e) => {
            console.log(e);
        }

        return () => {
            socket?.close();
        };
    }, [socket]);
    
    const createGame = () => {
        const event = {
            "Type": "NewGameRequest",
            "Data": {}
        }
        const str = JSON.stringify(event);
        console.log(str)
        if (socket == null)
            throw new Error("dupa")
        socket.send(str)
    }
    
    return <div className={game_elements.redealButton}>
        <button onClick={socketService}>Connect</button>
        <button onClick={createGame}>New Game</button>
    </div>
};
