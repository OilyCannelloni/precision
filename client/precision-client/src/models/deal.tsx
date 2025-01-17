export enum Position {
    West = "West", 
    North = "North",
    East = "East",
    South = "South"
}
export enum Suit {
    None = "x",
    Pass = "p",
    Clubs = "c",
    Diamonds = "d",
    Hearts = "h",
    Spades = "s",
    NT = "n"
}

export enum CardSuit {
    Clubs = "Clubs",
    Diamonds = "Diamonds",
    Hearts = "Hearts",
    Spades = "Spades",
}

export namespace CardSuitHelper {
    export function fromString(s: string) {
        return {
            "c": CardSuit.Clubs,
            "d": CardSuit.Diamonds,
            "h": CardSuit.Hearts,
            "s": CardSuit.Spades,
        }[s] ?? CardSuit.Clubs
    }
}

export namespace Suit {
    export function fromString(s: string) {
        return {
            "p": Suit.Pass,
            "c": Suit.Clubs,
            "d": Suit.Diamonds,
            "h": Suit.Hearts,
            "s": Suit.Spades,
            "n": Suit.NT
        }[s] || Suit.None
    }
}

export class Card {
    Value: string = "";
    Suit: CardSuit;
    cardStr = () => this.Value + this.Suit[0].toLowerCase();

    public constructor(value: string, suit: CardSuit) {
        this.Value = value
        this.Suit = suit;
    }
}

export class Hand {
    [CardSuit.Spades]: string;
    [CardSuit.Hearts]: string;
    [CardSuit.Diamonds]: string;
    [CardSuit.Clubs]: string;
    
    public constructor(s: string, h: string, d: string, c: string) {
        this[CardSuit.Spades] = s
        this[CardSuit.Hearts] = h
        this[CardSuit.Diamonds] = d
        this[CardSuit.Clubs] = c
    }
}

export class Deal {
    [Position.West]: Hand;
    [Position.North]: Hand;
    [Position.East]: Hand;
    [Position.South]: Hand;

    public constructor(w: Hand, n: Hand, e: Hand, s: Hand) {
        this[Position.West] = w
        this[Position.North] = n
        this[Position.East] = e
        this[Position.South] = s
    }
    
    public static fromStr(ss: string): Deal {
        const [w, n, e, s] = ss.split(" ").map(hs => {
            const [s, h, d, c] = hs.split(".")
            return new Hand(s, h, d, c)
        })
        return new Deal(w, n, e, s)
    }
}

export class Trick {
    [Position.West]: Card | null = null;
    [Position.North]: Card | null = null;
    [Position.East]: Card | null = null;
    [Position.South]: Card | null = null;

    public constructor(init?: Partial<Trick>) {
        Object.assign(this, init);
    }
}

export enum Vulnerability {
    None, NS, EW, All
}

export interface IDealBox {
    Deal: Deal;
    Number: number;
    Vulnerability: Vulnerability;
    Dealer: Position;
}
