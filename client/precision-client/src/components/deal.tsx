import game_elements from "./game_elements.module.css"
import {Hand, HandModel} from "./hand"

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

export function Deal(deal: DealModel) {
    return <div className={game_elements.deal}>
        <Container>
            <Row>
                <Col xl></Col>
                <Col xl>{Hand(deal.North)}</Col>
                <Col xl></Col>
            </Row>
            <Row>
                <Col xl>{Hand(deal.West)}</Col>
                <Col xl></Col>
                <Col xl>{Hand(deal.East)}</Col>
            </Row>
            <Row>
                <Col xl></Col>
                <Col xl>{Hand(deal.South)}</Col>
                <Col xl></Col>
            </Row>
        </Container>
    </div>
}