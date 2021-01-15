open Core.DomainTypes
open Core.Constants
open Variants

let log message = printfn "%s" message

module Alpha =

    let drawTriangle() =
        let turtle = Alpha.Turtle(log)

        turtle.Move 100.0
        turtle.Turn 120.0<Degrees>
        turtle.Move 100.0
        turtle.Turn 120.0<Degrees>
        turtle.Move 100.0
        turtle.Turn 120.0<Degrees>

    let drawPolygon n =
        let turtle = Alpha.Turtle(log)

        // why the 180 degree offset?
        // let angle = (360.0/float n) * 1.0<Degrees>
        let angle = (180.0 - (360.0/float n)) * 1.0<Degrees>

        let drawOneSide() =
            turtle.Move 100.0
            turtle.Turn angle

        for _ in [1..n] do
            drawOneSide()

module Beta =

    ///
    /// Comments
    /// Assigning the values in the constructor(s) seems clearly preferable here
    /// since callers have less work to do, and in this case you'd prefer to keep
    /// methods in sync and intended use is less work and easier...
    ///
    /// Immutable state is nice though...
    ///
    let move = Beta.Turtle.move log
    let turn = Beta.Turtle.turn log
    let penDown = Beta.Turtle.penDown log
    let penUp = Beta.Turtle.penUp log
    let setColor = Beta.Turtle.setColor log

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

open System

// TODO: write some tests of the turtle...

[<EntryPoint>]
let main _ =
    // Alpha.drawTriangle()
    // Beta.drawTriangle()

    Alpha.drawPolygon(5)
    Console.WriteLine ""
    Beta.drawPolygon(5)

    Console.WriteLine ""
    Console.WriteLine "Done. Press any key to close."
    Console.ReadLine() |> ignore
    0
