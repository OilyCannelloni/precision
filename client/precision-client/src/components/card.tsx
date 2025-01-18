"use client"

import game_elements from "./game_elements.module.scss"
import {Card, CardSuit} from "@/models/deal"


export function CardPlaceholder() {
    return <div className={game_elements.cardPlaceHolder} key={"42"}>
    </div>
}


export function CardComponent(card: Card) {
    const color_class = {
        [CardSuit.Spades]: "text-blue-700",
        [CardSuit.Hearts]: "text-red-500",
        [CardSuit.Diamonds]: "text-orange-600",
        [CardSuit.Clubs]: "text-green-600"
    }[card.Suit] ?? "";
    
    const pip = {
        [CardSuit.Spades]: "♠",
        [CardSuit.Hearts]: "♥",
        [CardSuit.Diamonds]: "♦",
        [CardSuit.Clubs]: "♣"
    }[card.Suit] ?? "";
    
    function cardClicked() {
        const event = new CustomEvent('cardClicked', {detail: card})
        window.dispatchEvent(event)
    }
    
    console.log(card)
    
    return <div className={game_elements.card} key={card.cardStr()} onClick={cardClicked}>
        <div className={`${game_elements.cardText} ${color_class}`}>
            {card.Value}
        </div>
        <div className={`${game_elements.cardText} ${color_class}`}>
            {pip}
        </div>
    </div>
}