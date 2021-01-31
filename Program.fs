open System
open Programs

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


[<EntryPoint>]
let main _ =
    // TODO: write some comparison tests

    let alpha3State = Alpha.drawTriangle()
    let beta3State = Beta.drawTriangle()
    let gamma3State = Gamma.drawTriangle()
    let delta3State = Delta.drawTriangle()
    let epislon3State = Epislon.drawTriangle()

    let alpha5State =  Alpha.drawPolygon 5
    let beta5State =  Beta.drawPolygon 5
    let gamma5State = Gamma.drawPolygon 5
    let delta5State = Delta.drawPolygon 5
    let epislon5State = Epislon.drawPolygon 5

    Console.WriteLine ""

    Console.WriteLine ""
    Console.WriteLine "Done. Press any key to close."
    Console.ReadLine() |> ignore
    0
