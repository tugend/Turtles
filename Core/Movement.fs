module Core.Movement

open Core.DomainTypes
open System

// round a float to two places to make it easier to read
let private round2 (x: float) = Math.Round(x, 2)

/// calculate a new position from the current position given an angle and a distance
let calculateMove (distance: Distance) (angle: Angle) (currentPos: Position) =
    // Convert degrees to radians with 180.0 degrees = 1 pi radian
    let angleInRadians =
        angle * (Math.PI / 180.0) * 1.0<1/Degrees>

    // current pos
    let x0 = currentPos.X
    let y0 = currentPos.Y

    // new pos
    let x1 = x0 + (distance * cos angleInRadians)
    let y1 = y0 + (distance * sin angleInRadians)

    // return a new Position
    { X = round2 x1; Y = round2 y1 }
