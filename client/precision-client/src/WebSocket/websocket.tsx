"use client";

import { useEffect } from 'react';
import game_elements from "@/components/game_elements.module.scss"

export function Connect({url} : {url: string}) {
    var socket: WebSocket | null = null;
    
    const dupa = useEffect(() => {
        socket = new WebSocket(url, "json");
        
        socket.onopen = () => {
            console.log('Connected to server');
        };
        
        socket.onmessage = (msg) => {
            console.log(msg.data)
            const event = JSON.parse(msg.data);
            console.log(event.Data)
            if (event.Data)
                event.Data = JSON.parse(event.data)
                
            console.log(event);
            
        }
        
        socket.onerror = (e) => {
            console.log(e);
        }

        return () => {
            socket?.close();
        };
    }, []);
    
    const createGame = () => {
        const event = {
            "Type": "NewGameRequest",
            "Data": {}
        }
        const str = JSON.stringify(event);
        console.log(str)
        socket?.send(str)
    }

    return <div className={game_elements.redealButton}>
        <button onClick={dupa}>Connect</button>
        <button onClick={createGame}>New Game</button>
    </div>
};
