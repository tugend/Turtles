module Agents.Epsilon

open Core.DomainTypes
open Variants.Beta

type TurtleCommand =
    | Move of Distance
    | Turn of Angle
    | PenUp
    | PenDown
    | SetColor of PenColor

type TurtleAgent() =
    let log message = printfn "%s" message

    let move = Turtle.move log
    let turn = Turtle.turn log
    let penDown = Turtle.penDown log
    let penUp = Turtle.penUp log
    let setColor = Turtle.setColor log

    let mailboxProc = MailboxProcessor.Start(fun inbox ->
        let rec loop turtleState = async {
            let! command = inbox.Receive()

            let newState =
                match command with
                | Move distance ->
                    move distance turtleState
                | Turn angle ->
                    turn angle turtleState
                | PenUp ->
                    penUp turtleState
                | PenDown ->
                    penDown  turtleState
                | SetColor color ->
                    setColor color turtleState

            return! loop newState
        }

        loop Turtle.initialTurtleState
    )

    member _.Post(command) =
        do mailboxProc.Post command