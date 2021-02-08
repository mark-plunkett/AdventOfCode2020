open System

module Part1 =

    let solve (lines:string) =
        lines.Replace("\r", "").Split("\n\n")
        |> Array.map (fun s -> s.Replace("\n", "") |> Seq.distinct |> Seq.length)
        |> Array.reduce (+)

[<EntryPoint>]
let main argv =
    let path = argv.[0]
    IO.File.ReadAllText(path)
    |> Part1.solve
    |> printfn "%A"

    0