﻿import game_elements from "./game_elements.module.scss"
import {HandComponent} from "./hand"
import {CardComponent, CardPlaceholder} from "./card"
import {Container, Row, Col} from "reactstrap"
import {useContext} from "react"
import {HookContext} from "@/common/hooks"
import {Trick, Position} from "@/models/deal"


function DealMiddleComponent({trick} : {trick: Trick}) {
    const north = trick[Position.North] ? CardComponent(trick[Position.North]) : CardPlaceholder();
    const west = trick[Position.West] ? CardComponent(trick[Position.West]) : CardPlaceholder();
    const east = trick[Position.East] ? CardComponent(trick[Position.East]) : CardPlaceholder();
    const south = trick[Position.South] ? CardComponent(trick[Position.South]) : CardPlaceholder();
    
    return <div className={game_elements.dealMiddle}>
        <div className={game_elements.dealMiddleCardNorth}>{north}</div>
        <div className={game_elements.dealMiddleCardWest}>{west}</div>
        <div className={game_elements.dealMiddleCardEast}>{east}</div>
        <div className={game_elements.dealMiddleCardSouth}>{south}</div>
    </div>
}

export function DealComponent() {
    const hooks = useContext(HookContext)
    
    return <div className={game_elements.deal}>
        <Container>
            <Row>
                <Col xl></Col>
                <Col xl>{HandComponent(Position.North)}</Col>
                <Col xl></Col>
            </Row>
            <Row>
                <Col xl>{HandComponent(Position.West)}</Col>
                <Col xl className={game_elements.dealMiddleWrapper}>
                    <DealMiddleComponent trick={hooks.DealMiddle.Value}></DealMiddleComponent>
                </Col>
                <Col xl>{HandComponent(Position.East)}</Col>
            </Row>
            <Row>
                <Col xl></Col>
                <Col xl>{HandComponent(Position.South)}</Col>
                <Col xl>
                </Col>
            </Row>
        </Container>
    </div>
}