import Holding from "./holding"
import game_elements from "./game_elements.module.scss"
import {CardSuit, Position} from "@/models/deal"


export function HandComponent(position: Position) {
    return <div className={game_elements.hand}>
        {Holding(position, CardSuit.Spades)}
        {Holding(position, CardSuit.Hearts)}
        {Holding(position, CardSuit.Diamonds)}
        {Holding(position, CardSuit.Clubs)}
    </div>
}