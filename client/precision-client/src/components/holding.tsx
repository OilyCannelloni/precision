import game_elements from "./game_elements.module.scss"
import {Card, CardModel, CardPlaceholder} from "./card"
export default function Holding(holding: string, suit: string) {
    let cards = [...holding].map(c => Card(new CardModel(c + suit)));
    
    if (cards.length == 0)
        cards = [CardPlaceholder()]
    
    return <div className={game_elements.holding} key={suit + holding}>
        {cards}
    </div>
}