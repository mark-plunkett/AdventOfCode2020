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

let fastPart1 target ints =
    ints
    |> Seq.scan (fun (map, _) i ->
        match Map.tryFind i map with
        | Some v -> map, Some (i * v)
        | _ -> Map.add (target - i) i map, None
    ) (Map.empty, None)
    |> Seq.tryPick snd

let fastPart2 target ints =
    let sorted = 
        ints
        |> Seq.sort
        |> Seq.indexed
        |> Seq.cache
    sorted
    |> Seq.pick (fun (index, intValue) -> 
        sorted
        |> Seq.skip index
        |> Seq.map snd
        |> fastPart1 (target - intValue)
        |> Option.map ((*) intValue)
    )

[<EntryPoint>]
let main argv =

    let solve = 
        Array.tryHead argv 
        |> Option.defaultValue "-p1"
        |> function
            | "-p1" -> naievePart1
            | "-p2" -> naievePart2
            | "-fp1" -> fastPart1 2020 >> Option.get
            | "-fp2" -> fastPart2 2020
            | _ -> failwith "not supported"

    Seq.initInfinite (fun _ -> Console.ReadLine())
    |> Seq.takeWhile (String.IsNullOrEmpty >> not)
    |> Seq.map int
    |> solve
    |> printfn "%i"

    0