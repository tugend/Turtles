module Variants.Beta

open Core.Constants
open Core.Movement
open Core.DomainTypes

open Ui.MoveLogger

module Turtle =

    type TurtleState =
        { Position: Position
          Angle: float<Degrees>
          Color: PenColor
          PenState: PenState }

    let initialTurtleState =
        { Position = Defaults.position
          Angle = 0.0<Degrees>
          Color = Defaults.color
          PenState = Defaults.penState }

    let move log distance state =
        log (sprintf "Move %0.1f distance" distance)

        let newPosition =
            calculateMove distance state.Angle state.Position

        if state.PenState = Down then
            log
            <| moveToText state.Position newPosition state.Color

        { state with Position = newPosition }

    let turn log angle state =
        log (sprintf "Turn %0.1f" angle)
        let newAngle = (state.Angle + angle) % 360.0<Degrees>

        { state with Angle = newAngle }

    let penUp log state =
        log "Pen up"

        { state with PenState = Up }

    let penDown log state =
        log "Pen down"

        { state with PenState = Down }

    let setColor log color state =
        log (sprintf "SetColor %A" color)

        { state with Color = color }