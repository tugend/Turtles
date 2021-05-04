module Variants.Alpha

open Core.Constants
open Core.Movement
open Core.DomainTypes

type Turtle(render) =
    let mutable currentPosition = Defaults.position
    let mutable currentAngle = Defaults.angle
    let mutable currentColor = Defaults.color
    let mutable currentPenState = Defaults.penState

    member __.Move(distance) =
        let newPosition = calculateMove distance currentAngle currentPosition

        if currentPenState = Down then
            render currentPosition newPosition currentColor

        currentPosition <- newPosition

    member __.Turn(angle) =
        let newAngle = (currentAngle + angle) % 360.0<Degrees>
        currentAngle <- newAngle

    member __.PenUp() =
        currentPenState <- Up

    member __.PenDown() =
        currentPenState <- Down

    member __.SetColor(color) =
        currentColor <- color
