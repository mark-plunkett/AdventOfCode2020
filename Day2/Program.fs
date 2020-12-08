open System

let parseLine s =
    let r = System.Text.RegularExpressions.Regex.Match(s, "(?<min>\d+)-(?<max>\d+) (?<char>[a-z]): (?<input>[a-z]+)")
    (
        r.Groups.Item("min").Value |> int,
        r.Groups.Item("max").Value |> int,
        r.Groups.Item("char").Value |> char,
        r.Groups.Item("input").Value
    )

module Part1 =

    let isValid (min, max, c, input: string) =
        input
        |> Seq.groupBy id
        |> Seq.exists (fun (key, items) -> 
            key = c && Seq.length items >= min && Seq.length items <= max
        )

module Part2 =  

    let isValid (pos1, pos2, c, input: string) =
        input.[pos1 - 1] = c <> (input.[pos2 - 1] = c)
        
[<EntryPoint>]
let main _ =
    Seq.initInfinite (fun _ -> Console.ReadLine())
    |> Seq.takeWhile (String.IsNullOrEmpty >> not)
    |> Seq.map parseLine
    |> Seq.where Part2.isValid
    |> Seq.length
    |> printfn "%i"

    0