"use client"

import {useState, createContext, ReactNode, Dispatch, SetStateAction} from "react"
import {CardSuit, Position, DealMiddle} from "@/models/deal"


interface HookWrapper<T> {
    Value: T
    SetValue: Dispatch<SetStateAction<T>>
}

interface HandHooks {
    [CardSuit.Spades]: HookWrapper<string>
    [CardSuit.Hearts]: HookWrapper<string>
    [CardSuit.Diamonds]: HookWrapper<string>
    [CardSuit.Clubs]: HookWrapper<string>
}

interface DealHooks {
    [Position.West]: HandHooks
    [Position.North]: HandHooks
    [Position.East]: HandHooks
    [Position.South]: HandHooks
    DealMiddle: HookWrapper<DealMiddle>
}

export const HookContext = createContext({} as DealHooks)

export function HookProvider({ children } : {children: ReactNode}) {
     const hooks = (() => {
        const holdingHook = () => {
            const [val, set] = useState("")
            return {
                Value: val,
                SetValue: set
            }
        }

        const middleHook = () => {
            const [val, set] = useState({} as DealMiddle)
            return {
                Value: val,
                SetValue: set
            }
        }

        const handHooks = () => {
            return {
                [CardSuit.Spades]: holdingHook(),
                [CardSuit.Hearts]: holdingHook(),
                [CardSuit.Diamonds]: holdingHook(),
                [CardSuit.Clubs]: holdingHook()
            }
        }

        return {
            [Position.West]: handHooks(),
            [Position.North]: handHooks(),
            [Position.East]: handHooks(),
            [Position.South]: handHooks(),
            DealMiddle: middleHook()
        }
    })()

    
    
    return <HookContext.Provider value={hooks}>
        {children}
    </HookContext.Provider>
}