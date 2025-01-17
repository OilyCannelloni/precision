"use client"

import {DealComponent} from "@/components/deal"
import {CardSuit} from "@/models/deal"
import {Card, Deal, Trick} from "@/models/deal"
import {Connect} from "@/WebSocket/websocket"
import {useState} from "react"

import {HookProvider} from "@/common/hooks"

export default function Page() {
    const dealModel = Deal.fromStr("T983.743.A9.K964 Q752.AT82.QJT7.5 4.KQJ9.K8652.Q87 AKJ6.65.43.AJT32");
    const dealMiddleModel = new Trick({
        West: new Card("7", CardSuit.Diamonds),
        North: new Card("K", CardSuit.Diamonds),
    });
    
    const [dealData, setDealData] = useState(dealModel);
    
    return <HookProvider>
        <div>
            <DealComponent />
            <Connect />
        </div>
    </HookProvider>


}