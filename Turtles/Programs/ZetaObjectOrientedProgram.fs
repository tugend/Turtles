module Programs.ZetaObjectOriented

open Apis.ZetaObjectOriented

let normalSize(render) =
    let turtle = Variants.Alpha.Turtle(render)

    { new ITurtle with
        member _.Move dist = turtle.Move dist
        member _.Turn angle = turtle.Turn angle
        member _.PenUp() = turtle.PenUp()
        member _.PenDown() = turtle.PenDown()
        member _.SetColor color = turtle.SetColor color
    }

let halfSize(render) =
    let turtle = normalSize(render)

    { new ITurtle with
        member _.Move dist = turtle.Move (dist/2.0)
        member _.Turn angle = turtle.Turn angle
        member _.PenUp() = turtle.PenUp()
        member _.PenDown() = turtle.PenDown()
        member _.SetColor color = turtle.SetColor color
    }

let drawTriangle(api: Api) =
    api.Exec "Move 100"
    api.Exec "Turn 120"
    api.Exec "Move 100"
    api.Exec "Turn 120"
    api.Exec "Move 100"

let drawNormalTriangle(render) =
    let turtle = normalSize(render)
    let api = Api(turtle)
    drawTriangle(api)

let drawHalfSizeTriangle(render) =
    let turtle = halfSize(render)
    let api = Api(turtle)
    drawTriangle(api)