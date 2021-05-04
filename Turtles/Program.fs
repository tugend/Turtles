open System
open Programs
open Ui.MoveRecorder
open Ui.MoveLogger
open Core.DomainTypes

// expected drawTriangle output

// Move 100.0 distance
// Draw line from (0.0, 0.0) to (100.0, 0.0) using Black
// Turn 120.0
// Move 100.0 distance
// Draw line from (100.0, 0.0) to (50.0, 86.6) using Black
// Turn 120.0
// Move 100.0 distance
// Draw line from (50.0, 86.6) to (-0.0, -0.0) using Black


// expected drawPolygon 5 output

// Move 100.0 distance
// Draw line from (0.0, 0.0) to (100.0, 0.0) using Black
// Turn 108.0
// Move 100.0 distance
// Draw line from (100.0, 0.0) to (69.1, 95.1) using Black
// Turn 108.0
// Move 100.0 distance
// Draw line from (69.1, 95.1) to (-11.8, 36.3) using Black
// Turn 108.0
// Move 100.0 distance
// Draw line from (-11.8, 36.3) to (69.1, -22.4) using Black
// Turn 108.0
// Move 100.0 distance
// Draw line from (69.1, -22.4) to (100.0, 72.7) using Black
// Turn 108.0

let rec assertSameX name expected ys =
  match expected, ys with
  | [], [] ->
    printfn "Verified %s" name
    ()
  | x::xs, y::ys ->
    if x = y
    then assertSameX name xs ys
    else raise (Exception("Mismatching list element\n" + (sprintf "%A\n\n !=\n\n %A" x y)))
  | _ -> raise (Exception("Mismatching list sizes!"))

[<EntryPoint>]
let main _ =
    let expected3StateOutput = [
        {
            FromPosition = { X = 50.0; Y = 86.6 };
            ToPosition = { X = 0.0; Y = 0.0 };
            Color = Black
        };
        {
            FromPosition = { X = 100.0; Y = 0.0 };
            ToPosition = { X = 50.0; Y = 86.6 };
            Color = Black
        };
        {
            FromPosition = { X = 0.0; Y = 0.0 };
            ToPosition = { X = 100.0; Y = 0.0 };
            Color = Black
        };
    ]

    let assertSameX name ys = assertSameX name expected3StateOutput ys

    // TODO: write some comparison tests
    let staticRecorder = StaticRecorder;

    let alphaRecorder = Recorder()
    Alpha.drawTriangle alphaRecorder.Move
    let alpha3State = alphaRecorder.Buffer
    assertSameX "Alpha" alpha3State

    // Beta.drawTriangle() |> ignore
    // let beta3State = staticRecorder.Buffer
    // staticRecorder.Clear()
    // assertSameX "Beta" beta3State

    // Gamma.drawTriangle()
    // let gamma3State = staticRecorder.Buffer
    // staticRecorder.Clear()
    // assertSameX "Gamma" gamma3State

    // // TODO: move this to unit tests
    // Delta.drawTriangle()
    // let delta3State = staticRecorder.Buffer
    // staticRecorder.Clear()
    // assertSameX "Delta" delta3State

    // // TODO: READ THIS ONE http://tomasp.net/blog/csharp-async-gotchas.aspx/
    // Epislon.drawTriangle()
    // Async.Sleep(1000) |> Async.RunSynchronously
    // let epislon3State = staticRecorder.Buffer
    // staticRecorder.Clear()
    // assertSameX "Epislon" epislon3State

    Console.WriteLine "Object-oriented normal triangle"
    let zetaObjectOrientedRecorder = Recorder()
    ZetaObjectOriented.drawNormalTriangle printMove
    ZetaObjectOriented.drawNormalTriangle zetaObjectOrientedRecorder.Move
    let zeta3State = zetaObjectOrientedRecorder.Buffer
    assertSameX "Object-oriented Zeta" zeta3State

    Console.WriteLine "Functional normal triangle"
    let zetaFunctionalRecorder = Recorder()
    ZetaFunctional.drawNormalTriangle printMove
    ZetaFunctional.drawNormalTriangle zetaFunctionalRecorder.Move
    let zeta3State = zetaFunctionalRecorder.Buffer
    assertSameX "Functional Zeta" zeta3State

    Console.WriteLine "Object-oriented half triangle"
    Console.WriteLine ""
    ZetaObjectOriented.drawHalfSizeTriangle printMove

    Console.WriteLine "Functional half triangle"
    Console.WriteLine ""
    ZetaFunctional.drawHalfSizeTriangle printMove

    // let alpha5State =  Alpha.drawPolygon recorder.Move 5
    // let beta5State =  Beta.drawPolygon 5
    // let gamma5State = Gamma.drawPolygon 5
    // let delta5State = Delta.drawPolygon 5
    // let epislon5State = Epislon.drawPolygon 5

    Console.WriteLine ""

    Console.WriteLine ""
    Console.WriteLine "Done. Press any key to close."
    Console.ReadLine() |> ignore
    0
