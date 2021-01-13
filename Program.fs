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

        turtle

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

    // meh... same as just assigning it in the constructor
    // and less tedius since we don't have to do it and maintain it for ALL methods

    // initial state could also be moved to ctor if needed

    // all methods could still be static if want them too..

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

open System

// TODO: write some tests of the turtle...

[<EntryPoint>]
let main _ =
    Alpha.drawTriangle() |> ignore
    Beta.drawTriangle() |> ignore

    Console.WriteLine ""
    Console.WriteLine "Done. Press any key to close."
    Console.ReadLine() |> ignore
    0
