"use client"

import {Deal, DealModel, DealMiddleModel} from "@/components/deal"
import {CardModel} from "@/components/card"
import {Connect} from "@/WebSocket/websocket"
import {useState} from "react"


export default function Page() {
    const dealModel = DealModel.from_str("T983.743.A9.K964 Q752.AT82.QJT7.5 4.KQJ9.K8652.Q87 AKJ6.65.43.AJT32");
    const dealMiddleModel = new DealMiddleModel({
        West: new CardModel("7d"),
        North: new CardModel("Kd"),
    });
    
    const [dealData, setDealData] = useState(dealModel);
    
    return <div>
        <div>
            <Deal deal={dealData} dm={dealMiddleModel} />
        </div>
        <Connect url={"http://localhost:9696/ws"} setDealData={setDealData}/>
    </div>;
}