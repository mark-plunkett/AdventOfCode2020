open System

[<EntryPoint>]
let main argv =

    Seq.initInfinite (fun _ -> Console.ReadLine())
    |> Seq.takeWhile (String.IsNullOrEmpty >> not)
    |> Seq.map int
    |> Seq.toList
    |> fun ints -> 
        [ for i in ints do
            for j in ints do
                for k in ints do
                    match i, j, k with
                    | i, j, k when i + j + k = 2020 -> yield i * j * k
                    | _ -> () ]
    |> List.head
    |> printfn "%i"

    0