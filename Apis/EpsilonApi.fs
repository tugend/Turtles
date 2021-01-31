module Apis.Epislon

open Core.DomainTypes
open Variants
open Agents.Epsilon
open Result

type ResultBuilder() =
    member _.Bind(m:Result<'a,'error>, f:'a -> Result<'b,'error>) =
        bind f m
    member _.Zero() :Result<unit,'error> =
        Ok ()

let result = ResultBuilder()


type ErrorMessage =
    | InvalidDistance of string
    | InvalidAngle of string
    | InvalidColor of string
    | InvalidCommand of string

let parseDistance value =
    try
        Ok(float value)
    with _ -> Error (InvalidDistance value)

let private parseAngle value =
    try
        Ok ((float value) * 1.0<Degrees>)
    with _ -> Error (InvalidAngle value)

let private parseColor value =
    match value with
    | "Black" -> Ok Black
    | "Blue" -> Ok Blue
    | "Red" -> Ok Red
    | _ -> Error (InvalidColor value)

let log message = printfn "%s" message
let move = Beta.Turtle.move log
let turn = Beta.Turtle.turn log
let penDown = Beta.Turtle.penDown log
let penUp = Beta.Turtle.penUp log
let setColor = Beta.Turtle.setColor log

type TurtleApi() =
    let turtleAgent = TurtleAgent()

    member _.Exec(command: string) =
        let tokens =
            command.Split(' ')
            |> List.ofArray
            |> List.map (fun x -> x.Trim())

        let result =
            match tokens with
            | [ "Move"; value ] -> result {
                    let! distance = parseDistance value
                    let command = Move distance
                    turtleAgent.Post command
                }
            | [ "Turn"; value ] -> result {
                    let! angle = parseAngle value
                    let command = Turn angle
                    turtleAgent.Post command
                }
            | [ "Pen"; "Up" ] -> result {
                    let command = PenUp
                    turtleAgent.Post command
                }
            | [ "Pen"; "Down" ] -> result {
                    let command = PenDown
                    turtleAgent.Post command
                }
            | [ "SetColor"; value ] -> result {
                    let! color = parseColor value
                    let command = SetColor color
                    turtleAgent.Post command
                }
            | _ -> Error(InvalidCommand command)

        result


// TODO: what does fsx mean compared to fs extensions?