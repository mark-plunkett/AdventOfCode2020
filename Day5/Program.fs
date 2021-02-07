open System

let binStringToInt mapping s =
    s
    |> Seq.map mapping
    |> String.concat String.Empty
    |> fun i -> Convert.ToInt32(i, 2)

let parseRowCol rowMapper colMapper (s: string) =
    let row = s.Substring(0, 7) |> binStringToInt rowMapper
    let col = s.Substring(7, 3) |> binStringToInt colMapper
    row, col

let calculateSeatID row col = (row * 8) + col
let stringToSeatID rowMapper colMapper (s:string) =
    s |> parseRowCol rowMapper colMapper ||> calculateSeatID

module Part1 =

    let rowMapper = function
        | 'B' -> "1"
        | 'F' -> "0"

    let colMapper = function
        | 'R' -> "1"
        | 'L' -> "0" 

    let solve =
        Seq.map (stringToSeatID rowMapper colMapper)
        >> Seq.max

module Part2 =

    let intToBinString n =
        [ 0 .. 9 ]
        |> Seq.map (fun i ->
            let i' = pown 2. i |> int
            if i' &&& n = i' then "1" else "0")
        |> Seq.reduce (+)
        |> Seq.rev
        |> fun s -> String.Join("", s)

    let rowColMapper = function
        | 'B' | 'R' -> "1"
        | 'F' | 'L' -> "0"

    let solve =
        Seq.map (binStringToInt rowColMapper)
        >> Seq.sort
        >> Seq.pairwise
        >> Seq.find (fun (a, b) -> b > a + 1)
        >> fst
        >> ((+) 1)
        >> intToBinString
        >> stringToSeatID string string

[<EntryPoint>]
let main argv =
    let solver = if argv.Length = 0 || argv.[0] = "-p1" then Part1.solve else Part2.solve
    Seq.initInfinite (fun _ -> Console.ReadLine())
    |> Seq.takeWhile (String.IsNullOrEmpty >> not)
    |> solver
    |> printfn "%i"

    0
