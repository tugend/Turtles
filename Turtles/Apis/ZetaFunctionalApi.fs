module Apis.ZetaFunctional

open Core.DomainTypes
open Variants.Beta

// <START> Difference from Gamma
type TurtleFunctions = {
    move : Distance -> Turtle.TurtleState -> Turtle.TurtleState
    turn : Angle -> Turtle.TurtleState -> Turtle.TurtleState
    penUp : Turtle.TurtleState -> Turtle.TurtleState
    penDown : Turtle.TurtleState -> Turtle.TurtleState
    setColor : PenColor -> Turtle.TurtleState -> Turtle.TurtleState
}
// </End> Difference from Gamma

exception ParseException of string

let private parseDistance value =
    try
        float value
    with ex ->
        let msg = sprintf "Invalid distance '%s' [%s]" value ex.Message
        raise (ParseException msg)

let private parseAngle value =
    try
        (float value) * 1.0<Degrees>
    with ex ->
        let msg = sprintf "Invalid angle '%s' [%s]" value ex.Message
        raise (ParseException msg)

let private parseColor value =
    match value with
    | "Black" -> Black
    | "Blue" -> Blue
    | "Red" -> Red
    | _ ->
        let msg = sprintf "Color '%s' is not recognized" value
        raise (ParseException msg)

let recordMove = Ui.MoveRecorder.StaticRecorder.Move;

type Api(turtle: TurtleFunctions) =

    let mutable state = Turtle.initialTurtleState

    member _.Exec (command:string) =
        let tokens =
            command.Split(' ')
            |> List.ofArray
            |> List.map (fun x -> x.Trim())

        match tokens with
            | ["Move"; value] ->
                let distance = parseDistance value
                let newState = turtle.move distance state
                state <- newState
            | ["Turn"; value] ->
                let angle = parseAngle value
                let newState = turtle.turn angle state
                state <- newState
            | ["Pen"; "Up"] ->
                let newState = turtle.penUp state
                state <- newState
            | ["Pen"; "Down"] ->
                let newState = turtle.penDown state
                state <- newState
            | ["SetColor"; value] ->
                let color = parseColor value
                let newState = turtle.setColor color state
                state <- newState
            | _ ->
                let msg = sprintf "Instruction '%s' not recognized" command
                raise (ParseException msg)
