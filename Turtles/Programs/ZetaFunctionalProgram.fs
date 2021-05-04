module Programs.ZetaFunctional

open Apis.ZetaFunctional
open Variants.Beta

let normalSize(render): TurtleFunctions =
    {
        move =  Turtle.move render
        turn = Turtle.turn
        penUp = Turtle.penUp
        penDown = Turtle.penDown
        setColor = Turtle.setColor
    }

let halfSize(render) =
    let normalSize = normalSize(render)

    { normalSize with
        move = fun dist -> normalSize.move (dist/2.0)
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