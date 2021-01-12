module Variants.Alpha

open Core.Constants
open Core.Movement
open Core.DomainTypes
open Ui.MoveLogger

type Turtle(log) =
    let mutable currentPosition = Defaults.position
    let mutable currentAngle = Defaults.angle
    let mutable currentColor = Defaults.color
    let mutable currentPenState = Defaults.penState

    member __.Move(distance) =
        log (sprintf "Move %0.1f" distance)
        let newPosition = calculateMove distance currentAngle currentPosition

        if currentPenState = Down then
            log <| moveToText currentPosition newPosition currentColor

        currentPosition <- newPosition

    member __.Turn(angle) =
        log (sprintf "Turn %0.1f" angle)
        let newAngle = (currentAngle + angle) % 360.0<Degrees>
        currentAngle <- newAngle

    member __.PenUp() =
        log "Pen up"
        currentPenState <- Up

    member __.PenDown() =
        log "Pen down"
        currentPenState <- Down

    member __.SetColor(color) =
        log (sprintf "SetColor %A" color)
        currentColor <- color
