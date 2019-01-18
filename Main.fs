open System
open System.Numerics
open System.Drawing
open System.Windows.Forms

open Ray


let pixels nx ny =
    seq {
        for j = ny - 1 downto 0 do
            for i = 0 to nx - 1 do
                yield (i, j)
    }


let gradient nx ny =
    seq {
        for i, j in pixels nx ny do
            let color =
                Color.FromArgb
                    (255,
                     int (255.99f * (float32 i / float32 nx)),
                     int (255.99f * (float32 j / float32 ny)),
                     int (255.99f * 0.2f))
            yield (i, ny - 1 - j, color)
    }


let unitVector (vec : Vector3) =
    vec / vec.Length ()


let hitsSphere (center : Vector3) (radius : float32) (ray : Ray) =
    let oc = ray.origin - center
    let a = Vector3.Dot (ray.direction, ray.direction)
    let b = 2.f * Vector3.Dot (oc, ray.direction)
    let c = Vector3.Dot (oc, oc) - radius * radius
    let discriminant = b * b - 4.f * a * c
    discriminant > 0.f


let color (r: Ray) =
    if hitsSphere (Vector3 (0.0f, 0.0f, -1.f)) 0.5f r then
        Vector3 (1.f, 0.f, 0.f)
    else
        let unitDir = unitVector r.direction
        let t = 0.5f * (unitDir.Y + 1.0f)
        (1.0f - t) * Vector3 (1.0f, 1.0f, 1.0f) + t * Vector3 (0.5f, 0.7f, 1.0f)


let simpleCamera nx ny =
    let lowerLeft = Vector3 (-2.0f, -1.0f, -1.0f)
    let horizontal = Vector3 (4.0f, 0.0f, 0.0f)
    let vertical = Vector3 (0.0f, 2.0f, 0.0f)
    let origin = Vector3 (0.0f, 0.0f, 0.0f)

    seq {
        for i, j in pixels nx ny do
            let u = float32 i / float32 nx
            let v = float32 j / float32 ny
            let r : Ray =
                { origin = origin;
                  direction = lowerLeft + (u * horizontal) + (v * vertical) }
            let col = color r

            let color = Color.FromArgb
                            (255,
                             int (255.99f * col.X),
                             int (255.99f * col.Y),
                             int (255.99f * col.Z))
            yield (i, ny - 1 - j, color)
    }


[<EntryPoint>]
[<STAThread>]
let main args =
    Application.EnableVisualStyles ()

    use form = new Form (Width = 400, Height = 200, Text = "Eyedazzler")

    let nx, ny = 400, 200

    use img = new Bitmap (nx, ny)

    match args with
    | [||]
    | [|"camera"|] ->
        simpleCamera nx ny |> Seq.iter img.SetPixel
    | [|"gradient"|] ->
        gradient nx ny |> Seq.iter img.SetPixel
    | _ ->
        exit 1

    form.Paint.Add (fun e -> e.Graphics.DrawImage (img, 0, 0))

    Application.Run form
    0