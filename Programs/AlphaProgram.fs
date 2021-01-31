module Programs.Alpha

open Core.DomainTypes
open Variants

let log message = printfn "%s" message

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