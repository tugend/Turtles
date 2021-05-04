module Ui.MoveLogger

open Core.DomainTypes

let printMove (oldPos: Position) (newPos: Position) (color: PenColor) =
    printfn "Draw line from (%0.1f, %0.1f) to (%0.1f, %0.1f) using %A" oldPos.X oldPos.Y newPos.X newPos.Y color
