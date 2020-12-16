open System

let countTrees (grid:string[]) =
    let modulo = grid.[0].Length
    Seq.initInfinite
    >> Seq.takeWhile (fun (_, y) -> y < grid.Length)
    >> Seq.where (fun (x, y) -> grid.[y].[x % modulo] = '#')
    >> Seq.length
    
let solvePart1 (grid:string[]) =
    fun i -> (i * 3), i
    |> countTrees grid 

let solvePart2 (grid:string[]) =
    [
        fun i -> i, i
        fun i -> i * 3, i
        fun i -> i * 5, i
        fun i -> i * 7, i
        fun i -> i, i * 2
    ]
    |> Seq.map (countTrees grid)
    |> Seq.reduce (*)
    
[<EntryPoint>]
let main _ =

    Seq.initInfinite (fun _ -> Console.ReadLine())
    |> Seq.takeWhile (String.IsNullOrEmpty >> not)
    |> Seq.toArray
    |> solvePart2
    |> printfn "%i"

    0