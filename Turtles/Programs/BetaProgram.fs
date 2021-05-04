module Programs.Beta

open Core.DomainTypes
open Variants

///
/// Comments
/// Assigning the values in the constructor(s) seems clearly preferable here
/// since callers have less work to do, and in this case you'd prefer to keep
/// methods in sync and intended use is less work and easier...
///
/// Immutable state is nice though...
///
///
///
let log message = printfn "%s" message

let move = Beta.Turtle.move Ui.MoveRecorder.StaticRecorder.Move
let turn = Beta.Turtle.turn
let penDown = Beta.Turtle.penDown
let penUp = Beta.Turtle.penUp
let setColor = Beta.Turtle.setColor

let drawTriangle() =
    Beta.Turtle.initialTurtleState
    |> move 100.0
    |> turn 120.0<Degrees>
    |> move 100.0
    |> turn 120.0<Degrees>
    |> move 100.0
    |> turn 120.0<Degrees>

let drawPolygon n =
    let angle = (180.0 - (360.0/float n)) * 1.0<Degrees>

    let drawSide state _ =
        state
        |> move 100.0
        |> turn angle

    [1..n]
    |> drawSide Beta.Turtle.initialTurtleState
    |> ignore