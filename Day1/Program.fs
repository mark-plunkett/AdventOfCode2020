open System

let naievePart1 ints =
    ints
    |> Seq.toList
    |> fun ints -> 
        [ for i in ints do
            for j in ints do
                match i, j with
                | i, j when i + j = 2020 -> yield i * j
                | _ -> () ]
    |> List.head

let naievePart2 ints =
    ints
    |> Seq.toList
    |> fun ints -> 
        [ for i in ints do
            for j in ints do
                for k in ints do
                    match i, j, k with
                    | i, j, k when i + j + k = 2020 -> yield i * j * k
                    | _ -> () ]
    |> List.head

let fastPart1 ints =
    let map = 
        ints
        |> Seq.map (fun i -> 2020 - i, i)
        |> dict

    ints
    |> Seq.pick (fun i -> 
        match map.TryGetValue i with
        | (true, v) -> Some (i * v)
        | _ -> None
    )

[<EntryPoint>]
let main argv =

    let f = 
        Array.tryHead argv 
        |> Option.defaultValue "p1"
        |> function
            | "p1" -> naievePart1
            | "p2" -> naievePart2
            | "p1f" -> fastPart1
            | _ -> failwith "not supported"

    Seq.initInfinite (fun _ -> Console.ReadLine())
    |> Seq.takeWhile (String.IsNullOrEmpty >> not)
    |> Seq.map int
    |> Seq.toList
    |> f
    |> printfn "%i"

    0