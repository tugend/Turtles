module Variants.Beta

open Core.Constants
open Core.Movement
open Core.DomainTypes

module Turtle =

    type TurtleState =
        { Position: Position
          Angle: float<Degrees>
          Color: PenColor
          PenState: PenState }

    let initialTurtleState =
        { Position = Defaults.position
          Angle = 0.0<Degrees>
          Color = Defaults.color
          PenState = Defaults.penState }

    let move render distance state =
        let newPosition =
            calculateMove distance state.Angle state.Position

        if state.PenState = Down then
            render state.Position newPosition state.Color

        { state with Position = newPosition }

    let turn angle state =
        let newAngle = (state.Angle + angle) % 360.0<Degrees>

        { state with Angle = newAngle }

    let penUp state =
        { state with PenState = Up }

    let penDown state =
        { state with PenState = Down }

    let setColor color state =
        { state with Color = color }