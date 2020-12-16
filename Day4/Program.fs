open FSharpPlus
open System

let expected = Set.ofList [
    "byr"
    "iyr"
    "eyr"
    "hgt"
    "hcl"
    "ecl"
    "pid"
]

let tee f v =
    f v
    v

let isValid (passport:string) =
    let fields =
        passport.Split(' ')
        |> Seq.map (fun token -> token.Split(':').[0])
        |> Set.ofSeq

    Set.forall (fields.Contains) expected

let sanitize (s:string) =
    s
        .Replace("\r", "")
        .Replace("\n", " ")

[<EntryPoint>]
let main argv =

    let input = IO.File.ReadAllText(argv.[0])
    String.split ["\n\n"] input
    |> Seq.map sanitize
    |> tee (printfn "%A")
    |> Seq.filter isValid
    |> Seq.length
    |> printfn "%i" 

    0