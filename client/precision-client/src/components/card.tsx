import game_elements from "./game_elements.module.css"
export default function Card(value: string, suit: string) {
    const color_class = {
        "s": "text-blue-700",
        "h": "text-red-500",
        "d": "text-orange-600",
        "c": "text-green-600"
    }[suit];
    
    const pip = {
        "s": "♠",
        "h": "♥",
        "d": "♦",
        "c": "♣"
    }[suit]
    
    return <div className={game_elements.card} key={value + suit}>
        <div className={`${game_elements.cardText} ${color_class}`}>
            {value}
        </div>
        <div className={`${game_elements.cardText} ${color_class}`}>
            {pip}
        </div>
    </div>
}