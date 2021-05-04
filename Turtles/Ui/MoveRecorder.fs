module Ui.MoveRecorder

open Core.DomainTypes

type Move = { FromPosition: Position; ToPosition: Position; Color: PenColor }

type Recorder() =
    let mutable buffer: Move list = []

    member _.Move (oldPos: Position) (newPos: Position) (color: PenColor) =
        let newMove = {FromPosition = oldPos; ToPosition = newPos; Color = color}
        buffer <- newMove :: buffer // TODO: why return bool?
        // MoveLogger.printMove oldPos newPos color

    member _.Buffer = buffer

    member _.Clear() =
        buffer <- []

let StaticRecorder = Recorder()