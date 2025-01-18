"use client"

import {useState, createContext, ReactNode, Dispatch, SetStateAction} from "react"
import {CardSuit, Position, Trick, Card} from "@/models/deal"


class HookWrapper<T> {
    Value: T
    SetValue: Dispatch<SetStateAction<T>>
    
    constructor() {
        const [val, set] = useState(Object() as T)
        this.Value = val
        this.SetValue = set
    }
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
    DealMiddle: HookWrapper<Trick>
    CardClicked: HookWrapper<Card>
}


interface GlobalVariables {
    GameId: string
}

export const HookContext = createContext({} as DealHooks)
export const GlobalVarContext = createContext({} as GlobalVariables)

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
            const [val, set] = useState({} as Trick)
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
            DealMiddle: middleHook(),
            CardClicked: new HookWrapper<Card>()
        }
    })()

    
    const globals: GlobalVariables = {
        GameId: ""
    }
    
    return <GlobalVarContext.Provider value={globals}>
        <HookContext.Provider value={hooks}>
            {children}
        </HookContext.Provider>
    </GlobalVarContext.Provider> 
}