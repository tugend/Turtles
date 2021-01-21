module Api.Gamma

open Core.DomainTypes

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
            
type Api() =
    let log message = printfn "%s" message 
    let turtle = Variants.Alpha.Turtle(log)
    
    member this.Exec (command:string) =
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
