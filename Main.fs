open System
open System.Numerics
open System.Drawing
open System.Windows.Forms

open Ray
open Sphere
open Hittable


let gradient nx ny =
    seq {
        for i, j in Render.pixels nx ny do
            let color =
                Vector3 (float32 i / float32 nx, float32 j / float32 ny, 0.2f)
                |> Render.vec3ToColor
            yield (i, ny - 1 - j, color)
    }


let color (r : Ray) (world : IHittable) =
    match Hittable.hits world r 0.f Single.MaxValue with
    | Some hit ->
        0.5f * Vector3 (hit.normal.X + 1.f, hit.normal.Y + 1.f, hit.normal.Z + 1.f)
    | None ->
        let t = 0.5f * (r.direction.Y + 1.0f)   // scale Y to 0..1
        Vector3.Lerp (Vector3 (1.0f, 1.0f, 1.0f), Vector3 (0.5f, 0.7f, 1.0f), t)


let simpleCamera nx ny origin =
    let lowerLeft = Vector3 (-2.0f, -1.0f, -1.0f)
    let horizontal = Vector3 (4.0f, 0.0f, 0.0f)
    let vertical = Vector3 (0.0f, 2.0f, 0.0f)

    let world =
        { list =
            [ Sphere.Make (Vector3 (0.f, 0.f, -1.f)) 0.5f
              Sphere.Make (Vector3 (0.f, -30.5f, -1.f)) 30.0f ] }

    seq {
        for x, y in Render.pixels nx ny do
            let u = float32 x / float32 nx
            let v = float32 y / float32 ny
            let r = Ray.make origin (lowerLeft + u * horizontal + v * vertical)
            let color = color r world |> Render.vec3ToColor
            yield (x, ny - 1 - y, color)
    }


[<EntryPoint>]
[<STAThread>]
let main _ =
    Application.EnableVisualStyles ()

    let nx, ny = 800, 400

    use form = new Form (Width = nx, Height = ny, Text = "Eyedazzler")

    use img = new Bitmap (nx, ny)

    let origin = ref (Vector3 (0.f, 0.f, 0.f))

    let refresh newOrigin =
        simpleCamera nx ny newOrigin |> Seq.iter img.SetPixel
        origin := newOrigin
        form.Refresh ()

    refresh !origin

    form.Paint.Add (fun e ->
        e.Graphics.DrawImage (img, 0, 0)
    )

    form.KeyDown.Add (fun e ->
        let o = !origin
        match e.KeyCode with
        | Keys.W ->
            Some <| Vector3 (o.X, o.Y, o.Z - 0.1f)
        | Keys.S ->
            Some <| Vector3 (o.X, o.Y, o.Z + 0.1f)
        | Keys.A ->
            Some <| Vector3 (o.X - 0.1f, o.Y, o.Z)
        | Keys.D ->
            Some <| Vector3 (o.X + 0.1f, o.Y, o.Z)
        | Keys.ControlKey ->
            Some <| Vector3 (o.X, o.Y - 0.1f, o.Z)
        | Keys.Space ->
            Some <| Vector3 (o.X, o.Y + 0.1f, o.Z)
        | _ -> None
        |> Option.iter refresh
    )

    Application.Run form
    0