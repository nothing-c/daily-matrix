open System
open System.IO
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.AspNetCore.Http
open System.Text

let mainpage = File.ReadAllText("page.html")

[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)
    // printfn "%A" builder.Environment
    let app = builder.Build()

    app.MapGet("/", Func<_>(fun () -> Results.Text(mainpage,"text/html",Encoding.UTF8) )) |> ignore
    app.MapGet("/answer", Func<_>(fun x -> printfn "%A" x)) |> ignore
    // /answer?answer=...

    app.Run()

    0 // Exit code

