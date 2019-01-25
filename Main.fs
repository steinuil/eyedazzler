open System
open System.Numerics
open System.Drawing
open System.Windows.Forms

open Ray
open Sphere
open Hittable
open Camera


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


let simpleCamera nx ny samples =
    let camera = Camera.Default

    let world =
        { list =
            [ Sphere.Make (Vector3 (0.f, 0.f, -1.f)) 0.5f
              Sphere.Make (Vector3 (0.f, -30.5f, -1.f)) 30.0f ] }

    let random = new Random()

    let rand () =
        random.NextDouble () |> float32

    seq {
        for x, y in Render.pixels nx ny do
            let col =
                seq { 1 .. samples }
                |> Seq.fold (fun col _ ->
                    let u = (float32 x + rand ()) / float32 nx
                    let v = (float32 y + rand ()) / float32 ny
                    let r = Camera.getRay camera u v
                    col + color r world
                ) Vector3.Zero
            let col = col / float32 samples
            let color = Render.vec3ToColor col
            yield (x, ny - 1 - y, color)
    }


[<EntryPoint>]
[<STAThread>]
let main _ =
    Application.EnableVisualStyles ()

    let nx, ny = 1600, 800

    use form = new Form (Width = nx, Height = ny, Text = "Eyedazzler")

    use img = new Bitmap (nx, ny)

    simpleCamera nx ny 50 |> Seq.iter img.SetPixel

    form.Paint.Add (fun e ->
        e.Graphics.DrawImage (img, 0, 0)
    )

    Application.Run form
    0