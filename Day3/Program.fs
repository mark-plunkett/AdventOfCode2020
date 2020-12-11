open System
    
let solvePart1 (grid:string[]) =
    let m = grid.[0].Length
    Seq.initInfinite (fun i -> (i * 3) % m, i)
    |> Seq.takeWhile (fun (_, y) -> y < grid.Length)
    |> Seq.where (fun (x, y) -> grid.[y].[x] = '#')
    |> Seq.length

let solvePart2 (grid:string[]) =
    let m = grid.[0].Length
    [
        fun i -> i, i
        fun i -> i * 3, i
        fun i -> i * 5, i
        fun i -> i * 7, i
        fun i -> i, i * 2
    ]
    |> Seq.map (
        Seq.initInfinite
        >> Seq.takeWhile (fun (_, y) -> y < grid.Length)
        >> Seq.where (fun (x, y) -> grid.[y].[x % m] = '#')
        >> Seq.length
    )
    |> Seq.reduce (*)
    
[<EntryPoint>]
let main _ =

    Seq.initInfinite (fun _ -> Console.ReadLine())
    |> Seq.takeWhile (String.IsNullOrEmpty >> not)
    |> Seq.toArray
    |> solvePart2
    |> printfn "%i"

    0