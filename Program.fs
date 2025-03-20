open System
open System.IO
open System.Web
open System.Text
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.AspNetCore.Http
//open Microsoft.AspNet.StaticFiles

let mainpage = File.ReadAllText("page.html")

let answer (context: HttpContext) (correct: int) =
    let a = context.Request.Query in
    let actualanswer = (Seq.head a).Value |> Seq.head |> int in
    if actualanswer = correct then "Yay" else "Boo"

[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)
    let app = builder.Build()
    app.UseStaticFiles() |> ignore

    let correct = 0
    // app.MapGet("/", Func<_>(fun () -> Results.Text(mainpage,"text/html",Encoding.UTF8) )) |> ignore
    // /answer?answer=...

    app.MapGet("/answer", Func<HttpContext, string>(fun x -> answer x correct)) |> ignore
    // so Func<arg, arg..., returntype>

    app.Run()

    0 // Exit code

