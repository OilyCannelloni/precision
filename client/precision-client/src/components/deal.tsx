import game_elements from "./game_elements.module.scss"
import {Hand, HandModel} from "./hand"
import {Card, CardModel, CardPlaceholder} from "./card"
import {Container, Row, Col} from "reactstrap"

export class DealModel {
    West: HandModel = new HandModel();
    North: HandModel = new HandModel();
    East: HandModel = new HandModel();
    South: HandModel = new HandModel();
    
    public constructor(init?: Partial<DealModel>) {
        Object.assign(this, init);
    }
    
    public static from_str(s: string) {
        const hands = s.split(" ");
        return new DealModel({
            West: HandModel.from_str(hands[0]),
            North: HandModel.from_str(hands[1]),
            East: HandModel.from_str(hands[2]),
            South: HandModel.from_str(hands[3])
        });
    }
}

export class DealMiddleModel {
    West: CardModel | null = null;
    North: CardModel | null = null;
    East: CardModel | null = null;
    South: CardModel | null = null;

    public constructor(init?: Partial<DealMiddleModel>) {
        Object.assign(this, init);
    }
}

function DealMiddle(dm: DealMiddleModel) {
    const north = dm.North ? Card(dm.North) : CardPlaceholder();
    const west = dm.West ? Card(dm.West) : CardPlaceholder();
    const east = dm.East ? Card(dm.East) : CardPlaceholder();
    const south = dm.South ? Card(dm.South) : CardPlaceholder();
    
    return <div className={game_elements.dealMiddle}>
        <div className={game_elements.dealMiddleCardNorth}>{north}</div>
        <div className={game_elements.dealMiddleCardWest}>{west}</div>
        <div className={game_elements.dealMiddleCardEast}>{east}</div>
        <div className={game_elements.dealMiddleCardSouth}>{south}</div>
    </div>
}

export function Deal(deal: DealModel, dm: DealMiddleModel = new DealMiddleModel()) {
    return <div className={game_elements.deal}>
        <Container>
            <Row>
                <Col xl></Col>
                <Col xl>{Hand(deal.North)}</Col>
                <Col xl></Col>
            </Row>
            <Row>
                <Col xl>{Hand(deal.West)}</Col>
                <Col xl className={game_elements.dealMiddleWrapper}>
                    <DealMiddle {...dm}></DealMiddle>
                </Col>
                <Col xl>{Hand(deal.East)}</Col>
            </Row>
            <Row>
                <Col xl></Col>
                <Col xl>{Hand(deal.South)}</Col>
                <Col xl>
                </Col>
            </Row>
        </Container>
    </div>
}