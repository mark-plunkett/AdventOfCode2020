open System

module Part1 =
    let stringToInt mapping s =
        s
        |> Seq.map mapping
        |> String.concat String.Empty
        |> fun i -> Convert.ToInt32(i, 2)

    let calculateSeatID (s:string) =
        let row = s.Substring(0, 7) |> stringToInt (function | 'B' -> "1" | 'F' -> "0") 
        let col = s.Substring(7, 3) |> stringToInt (function | 'R' -> "1" | 'L' -> "0") 
        (row * 8) + col

    let solve seats =
        seats
        |> Seq.map calculateSeatID
        |> Seq.max
        |> printfn "%i"

[<EntryPoint>]
let main _ =

    Seq.initInfinite (fun _ -> Console.ReadLine())
    |> Seq.takeWhile (String.IsNullOrEmpty >> not)
    |> Part1.solve

    0