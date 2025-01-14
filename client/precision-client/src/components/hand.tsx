import Holding from "./holding"
import game_elements from "./game_elements.module.scss"

export class HandModel {
    Spades: string = "";
    Hearts: string = "";
    Diamonds: string = "";
    Clubs: string = "";
    
    public constructor(init?: Partial<HandModel>) {
        Object.assign(this, init);
    }
    
    public static from_str(s: string): HandModel {
        const holdings = s.split(".");
        return new HandModel({
            Spades: holdings[0],
            Hearts: holdings[1],
            Diamonds: holdings[2],
            Clubs: holdings[3]
        });
    }
}

export function Hand(hand: HandModel) {
    return <div className={game_elements.hand}>
        {Holding(hand.Spades, "s")}
        {Holding(hand.Hearts, "h")}
        {Holding(hand.Diamonds, "d")}
        {Holding(hand.Clubs, "c")}
    </div>
}