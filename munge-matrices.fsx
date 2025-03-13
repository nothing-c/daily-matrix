open System.IO
open System.Text.RegularExpressions
let dir = "./Ravens-Progressive-Matrices/Problems"
let subdirs x = List.ofSeq (Directory.EnumerateDirectories x)
let files = Directory.EnumerateFiles

let allproblems = List.map subdirs (subdirs dir) |> List.concat

// Thank goodness for case-sensitivity
let regex = new Regex(@"PNG|txt")

let perdir dir = let allfiles = Directory.EnumerateFiles dir in List.ofSeq allfiles |>  List.filter (fun x -> regex.IsMatch(x));;

// AND they're pre-sorted! w00t!
let matrixdata (m: string list) = let solution = File.ReadAllText m[1] in (m[0], solution)

List.map perdir allproblems |> List.map matrixdata |> printfn "%A"
