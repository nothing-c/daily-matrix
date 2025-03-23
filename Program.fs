open System
open System.IO
open System.Web
open System.Text
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.AspNetCore.Http
//open Microsoft.AspNet.StaticFiles

let mainpage = File.ReadAllText("page.html")

let answer (context: HttpContext) (correct: string) =
    let a = context.Request.Query in
    let actualanswer = (Seq.head a).Value |> Seq.head
    if actualanswer = correct then "Yay" else "Boo"

[<EntryPoint>]
let main args =
    let rand = new Random()
    let builder = WebApplication.CreateBuilder(args)
    let app = builder.Build()
    app.UseStaticFiles() |> ignore

    let (matrixpath, correct) = Matrices.rawmatrices[rand.Next(Matrices.rawmatrices.Length)]
    // let mutable correct = snd dailymatrix
    // All the images are PNGs
    app.MapGet("/matrix", Func<_>(fun () -> Results.Bytes(File.ReadAllBytes("wwwroot/" + matrixpath), "image/png"))) |> ignore

    // app.MapGet("/", Func<_>(fun () -> Results.Text(mainpage,"text/html",Encoding.UTF8) )) |> ignore
    // /answer?answer=...

    app.MapGet("/answer", Func<HttpContext, string>(fun x -> answer x correct)) |> ignore
    // so Func<arg, arg..., returntype>

    app.Run()

    0 // Exit code

