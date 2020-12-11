open System

let parseGridToList lines =
    let rec parseGridRec lines grid =
        match lines with
        | [] -> grid
        | line::remaining -> 
            let parsedLine = Seq.map (function | '.' -> false | '#' -> true) line
            parseGridRec remaining (parsedLine::grid)
    
    parseGridRec lines []
    |> List.rev

let isTree (grid:string[]) x y =
    grid.[y].[x] = '#'

let printGrid grid =
    grid
    |> List.iter (
        Seq.map (function | true -> '#' | false -> '.')
        >> Seq.toArray
        >> String
        >> printfn "%s"
    )
    
let solve (grid:string[]) =
    let m = grid.[0].Length
    Seq.initInfinite (fun i -> (i * 3) % m, i)
    |> Seq.takeWhile (fun (_, y) -> y < grid.Length)
    |> Seq.where (fun (x, y) -> grid.[x].[y] = '#')
    |> Seq.length

[<EntryPoint>]
let main _ =

    Seq.initInfinite (fun _ -> Console.ReadLine())
    |> Seq.takeWhile (String.IsNullOrEmpty >> not)
    |> Seq.toArray
    |> solve
    |> printfn "%i"

    0