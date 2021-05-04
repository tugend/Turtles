module Apis.ZetaObjectOriented

open Core.DomainTypes

// <START> Difference from Gamma
type ITurtle =
    abstract Move : Distance -> unit
    abstract Turn : Angle -> unit
    abstract PenUp : unit -> unit
    abstract PenDown : unit -> unit
    abstract SetColor : PenColor -> unit
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

type Api(turtle: ITurtle) =

    member _.Exec (command:string) =
        let tokens =
            command.Split(' ')
            |> List.ofArray
            |> List.map (fun x -> x.Trim())

        match tokens with
            | ["Move"; value] ->
                let distance = parseDistance value
                turtle.Move distance
            | ["Turn"; value] ->
                let angle = parseAngle value
                turtle.Turn angle
            | ["Pen"; "Up"] ->
                turtle.PenUp()
            | ["Pen"; "Down"] ->
                turtle.PenDown()
            | ["SetColor"; value] ->
                let color = parseColor value
                turtle.SetColor color
            | _ ->
                let msg = sprintf "Instruction '%s' not recognized" command
                raise (ParseException msg)
