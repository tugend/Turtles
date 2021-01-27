module Apis.Epislon

open Core.DomainTypes
open Variants
open Agents.Epsilon
open Result

type ResultBuilder() =
    member this.Bind(m:Result<'a,'error>,f:'a -> Result<'b,'error>) =
        Result.bind f m
    member this.Return(x) :Result<'a,'error> =
        Ok x
    member this.ReturnFrom(m) :Result<'a,'error> =
        m
    member this.Zero() :Result<unit,'error> =
        this.Return ()
    member this.Combine(m1, f) =
        this.Bind(m1, f)
    member this.Delay(f) =
        f
    member this.Run(m) =
        m()
    member this.TryWith(m:Result<'a,'error>, h: exn -> Result<'a,'error>) =
        try this.ReturnFrom(m)
        with e -> h e
    member this.TryFinally(m:Result<'a,'error>, compensation) =
        try this.ReturnFrom(m)
        finally compensation()
    // member this.Using(res:#IDisposable, body) : Result<'b,'error> =
    //     this.TryFinally(body res, (fun () -> match res with null -> () | disp -> disp.Dispose()))
    member this.While(cond, m) =
        if not (cond()) then
            this.Zero()
        else
            this.Bind(m(), fun _ -> this.While(cond, m))
    // member this.For(sequence:seq<_>, body) =
    //     this.Using(sequence.GetEnumerator(),
    //         (fun enum -> this.While(enum.MoveNext, fun _ -> body enum.Current)))

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

// JESUS FUCKING CHRIST, THIS LOOKS SOOOO MUCH EASIER TO READ THAT THAN SILLY FUNCTIONAL COMPOSITION CRAP
let lift2R f xR yR =
    match xR, yR with
    | Error e, _ -> Error e
    | _, Error e -> Error e
    | Ok x, Ok y -> Ok(f x y)

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
            | _ -> Error(InvalidCommand command)


        result


// TODO: what does fsx mean compared to fs extensions?