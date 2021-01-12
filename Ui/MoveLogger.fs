module Ui.MoveLogger

open Core.DomainTypes

let moveToText (oldPos: Position) (newPos: Position) (color: PenColor) =
    // for now just log it
    sprintf "Draw line from (%0.1f, %0.1f) to (%0.1f, %0.1f) using %A" oldPos.X oldPos.Y newPos.X newPos.Y color
