open System

module Part1 =

    let solve (lines:string) =
        lines.Replace("\r", "").Split("\n\n")
        |> Array.map (fun s -> s.Replace("\n", "") |> Seq.distinct |> Seq.length)
        |> Array.reduce (+)

module Part2 =
    let countCommonAnswers (group:string) =
        let people = group.Split("\n")
        people
        // count the number of letters that are similar between all lines in group
        |> Array.fold (fun map s ->
            Map.tryFind s map
            |> Option.map ((+) 1)
            |> Option.defaultValue 0
            |> Map.add s
        ) Map.empty
        |> Map.toSeq
        |> Seq.map snd
        |> Seq.filter ((=) people.Length)
        |> Seq.reduce (+)

    let solve (lines:string) =
        lines.Replace("\r", "").Split("\n\n")
        |> Array.map countCommonAnswers
        |> Array.reduce (+)

[<EntryPoint>]
let main argv =
    let path = argv.[0]
    IO.File.ReadAllText(path)
    |> Part1.solve
    |> printfn "%A"

    0