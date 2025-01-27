"use client"

import {DealComponent} from "@/components/deal"
import {Connect} from "@/WebSocket/websocket"
import {HookProvider} from "@/common/hooks"

export default function Page() {
    return <HookProvider>
        <div>
            <DealComponent />
            <Connect />
        </div>
    </HookProvider>
}