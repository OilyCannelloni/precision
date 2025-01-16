import game_elements from "./game_elements.module.scss"

export class CardModel {
    Value: string = "";
    Suit: string = "";
    cardStr = () => this.Value + this.Suit;
    
    public constructor(cardString: string) {
        this.Value = cardString[0];
        this.Suit = cardString[1];
    }
}

export function CardPlaceholder() {
    return <div className={game_elements.cardPlaceHolder}>
    </div>
}


export function Card(cardModel: CardModel) {
    const color_class = {
        "s": "text-blue-700",
        "h": "text-red-500",
        "d": "text-orange-600",
        "c": "text-green-600"
    }[cardModel.Suit] ?? "";
    
    const pip = {
        "s": "♠",
        "h": "♥",
        "d": "♦",
        "c": "♣"
    }[cardModel.Suit] ?? "";
    
    return <div className={game_elements.card} key={cardModel.cardStr()}>
        <div className={`${game_elements.cardText} ${color_class}`}>
            {cardModel.Value}
        </div>
        <div className={`${game_elements.cardText} ${color_class}`}>
            {pip}
        </div>
    </div>
}