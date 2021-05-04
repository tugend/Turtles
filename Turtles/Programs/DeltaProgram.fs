module Programs.Delta

open System

let throwOnError result =
    match result with
        | Error x -> raise (InvalidOperationException (sprintf "%A" x)) |> ignore
        | Ok _ -> ()

let drawTriangle() =
    let api = Apis.Delta.TurtleApi()
    let exec = api.Exec >> throwOnError

    exec "Move 100"
    exec "Turn 120"
    exec "Move 100"
    exec "Turn 120"
    exec "Move 100"

let drawPolygon n =
    let angle = 180.0 - (360.0/float n)

    let api = Apis.Delta.TurtleApi()
    let exec = api.Exec >> throwOnError

    let drawSide() =
        exec "Move 100.0"
        exec (sprintf "Turn %f" angle)

    for _ in [1..n] do
        drawSide()