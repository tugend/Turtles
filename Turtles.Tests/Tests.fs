module Tests

open Xunit
open System
open Core.DomainTypes
open Ui.MoveRecorder
open Programs

// TODO
let rec assertSame name expected ys =
  match expected, ys with
  | [], [] ->
    printfn "Verified %s" name
    ()
  | x::xs, y::ys ->
    if x = y
    then assertSame name xs ys
    else raise (Exception("Mismatching list element\n" + (sprintf "%A\n\n !=\n\n %A" x y)))
  | _ -> raise (Exception("Mismatching list sizes!"))

let expected3StateOutput  = [
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


// TODO: write some comparison tests

[<Fact>]
let ``My test`` () =
  let recorder = Recorder()
  let assertSame name ys = assertSame name expected3StateOutput ys
  Alpha.drawTriangle recorder.Move
  let alpha3State = recorder.Buffer
  assertSame "Alpha" alpha3State
  Assert.True(true)

  // TODO: get the test to run correctly
  // TODO: what's the value of the sln file?
