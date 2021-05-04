module Apis.Delta

open Core.DomainTypes
open Variants

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

// JESUS FUCKING CHRIST, THIS LOOKS SOOOO MUCH EASIER TO READ THAT THAN SILLY FUNCTIONAL COMPOSITION CRAP
let lift2R f xR yR =
    match xR, yR with
    | Error e, _ -> Error e
    | _, Error e -> Error e
    | Ok x, Ok y -> Ok(f x y)

let recordMove = Ui.MoveRecorder.StaticRecorder.Move;
let move = Beta.Turtle.move recordMove
let turn = Beta.Turtle.turn
let penDown = Beta.Turtle.penDown
let penUp = Beta.Turtle.penUp
let setColor = Beta.Turtle.setColor

type TurtleApi() =
    let mutable state = Beta.Turtle.initialTurtleState

    let updateState newState = state <- newState

    member _.Exec(command: string) =
        let tokens =
            command.Split(' ')
            |> List.ofArray
            |> List.map (fun x -> x.Trim())

        let stateR = Ok state

        let newStateR =
            match tokens with
            | [ "Move"; value ] ->
                let distanceR = parseDistance value
                lift2R move distanceR stateR
            | ["Turn"; value] ->
                let angleR = parseAngle value
                lift2R turn angleR stateR
            | ["Pen"; "Up"] ->
                Ok (penUp state)
            | ["Pen"; "Down"] ->
                Ok (penDown state)
            | ["SetColor"; value] ->
                let colorR = parseColor value
                lift2R setColor colorR stateR
            | _ -> Error(InvalidCommand command)

        Result.map updateState newStateR
