open FSharpPlus
open System
open System.Collections.Generic

let expectedFields = Set.ofList [
    "byr"
    "iyr"
    "eyr"
    "hgt"
    "hcl"
    "ecl"
    "pid"
]

let isValidPart1 (passport:string) =
    let fields =
        passport.Split(' ')
        |> Seq.map (fun token -> token.Split(':').[0])
        |> Set.ofSeq

    Set.forall (fields.Contains) expectedFields

let areValuesValid (values:IDictionary<string, string>) =
    let isValueValid key value =
        match key with
        | "byr" -> value |> int |> fun i -> i >= 1920 && i <= 2002
        | "iyr" -> value |> int |> fun i -> i >= 2010 && i <= 2020
        | "eyr" -> value |> int |> fun i -> i >= 2020 && i <= 2030
        | "hgt" -> true
        // Incomplete matches but cba

    values
    |> Dict.keys
    |> Seq.forall (fun key -> isValueValid key values.[key]) 

let isValidPart2 (passport:string) =
    let fields =
        passport.Split(' ')
        |> Seq.map (String.split [":"] >> Seq.toList >> fun [a;b] -> a, b)
        |> dict
    
    Set.forall (fields.ContainsKey) expectedFields
    && areValuesValid fields

let sanitize (s:string) =
    s
        .Replace("\r", "")
        .Replace("\n", " ")

[<EntryPoint>]
let main argv =

    let input = IO.File.ReadAllText(argv.[0])
    String.split ["\n\n"] input
    |> Seq.map sanitize
    |> Seq.filter isValidPart1
    |> Seq.length
    |> printfn "%i" 

    0