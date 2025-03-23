open System
open System.IO
open System.Web
open System.Text
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.AspNetCore.Http

let mainpage = File.ReadAllText("page.html")
let rand = new Random()

let answer (context: HttpContext) (correct: string) =
    let a = context.Request.Query in
    let actualanswer = (Seq.head a).Value |> Seq.head
    if actualanswer = correct then "Yay" else "Boo"

[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)
    let app = builder.Build()
    app.UseStaticFiles() |> ignore

    let mutable (matrixpath, correct) = Matrices.rawmatrices[rand.Next(Matrices.rawmatrices.Length)] //Grab the first one
    // All the images are PNGs
    app.MapGet("/matrix", Func<_>(fun () -> Results.Bytes(File.ReadAllBytes("wwwroot/" + matrixpath), "image/png"))) |> ignore
    app.MapGet("/answer", Func<HttpContext, string>(fun x -> answer x correct)) |> ignore

    app.Run()

    let mutable day = DateTime.UtcNow.DayOfWeek
    while true do
        if day <> DateTime.UtcNow.DayOfWeek
        then
            day <- DateTime.UtcNow.DayOfWeek
            // full-sending this, so it may do two of the same in a row. Who cares?
            let (matrixpath', correct') = Matrices.rawmatrices[rand.Next(Matrices.rawmatrices.Length)]
            matrixpath <- matrixpath'
            correct <- correct'

    0 // Exit code

