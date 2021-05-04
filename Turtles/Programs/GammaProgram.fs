module Programs.Gamma

/// I don't buy into the whole 'system is very stateful' as a critique
/// To me this looks like a fine implementation and if I'd want to 'decoupl' the
/// coupling between the api and the turtle implementation, I'd use a turtle interface
/// and assign it via the ctor, easy!
///
///
let drawTriangle() =
    let api = Apis.Gamma.Api()

    api.Exec "Move 100"
    api.Exec "Turn 120"
    api.Exec "Move 100"
    api.Exec "Turn 120"
    api.Exec "Move 100"

let drawPolygon n =
    let angle = 180.0 - (360.0/float n)
    let api = Apis.Gamma.Api()

    let drawSide() =
        api.Exec "Move 100.0"
        api.Exec (sprintf "Turn %f" angle)

    for _ in [1..n] do
        drawSide()
