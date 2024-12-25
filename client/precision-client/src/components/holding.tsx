import game_elements from "./game_elements.module.css"
import Card from "./card"
export default function Holding(holding: string, suit: string) {
    const cards = [...holding].map(c => Card(c, suit));
    
    return <div className={game_elements.holding} key={suit + holding}>
        {cards}
    </div>
}