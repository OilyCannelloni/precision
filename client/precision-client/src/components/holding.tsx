import game_elements from "./game_elements.module.scss"
import {CardComponent, CardPlaceholder} from "./card"
import {CardSuit, Card, Position} from "@/models/deal"
import {useContext} from "react" 
import {HookContext} from "@/common/hooks"

export default function Holding(position: Position, suit: CardSuit) {
    const hooks = useContext(HookContext)
    const holding = hooks[position][suit].Value
    
    let cards = [...holding].map(c => CardComponent(new Card(c, suit)));
    if (cards.length == 0)
        cards = [CardPlaceholder()]
    
    return <div className={game_elements.holding} key={suit + holding}>
        {cards}
    </div>
}