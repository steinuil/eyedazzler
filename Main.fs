open System
open System.Numerics
open System.Drawing
open System.Windows.Forms

open Ray


/// Yields coordinates from (0, ny) to (nx, 0)
let pixels nx ny =
    seq {
        for j = ny - 1 downto 0 do
            for i = 0 to nx - 1 do
                yield (i, j)
    }


let vector3ToColor (vec : Vector3) =
    Color.FromArgb
        (255,
         int (255.99f * vec.X),
         int (255.99f * vec.Y),
         int (255.99f * vec.Z))


let gradient nx ny =
    seq {
        for i, j in pixels nx ny do
            let color =
                Vector3 (float32 i / float32 nx, float32 j / float32 ny, 0.2f)
                |> vector3ToColor
            yield (i, ny - 1 - j, color)
    }


let unitVector (vec : Vector3) =
    vec / vec.Length ()


let hitSphere (center : Vector3) (radius : float32) (ray : Ray) =
    let oc = ray.origin - center
    let a = Vector3.Dot (ray.direction, ray.direction)
    let b = 2.f * Vector3.Dot (oc, ray.direction)
    let c = Vector3.Dot (oc, oc) - radius * radius
    let discriminant = b * b - 4.f * a * c
    if discriminant < 0.f then
        None
    else
        Some ((-b - sqrt discriminant) / (2.f * a))


let color (r : Ray) =
    match hitSphere (Vector3 (0.0f, 0.0f, -1.f)) 0.5f r with
    | Some hit ->
        let n = unitVector ((Ray.pointAt hit r) - Vector3 (0.f, 0.f, -1.f))
        0.5f * Vector3 (n.X + 1.f, n.Y + 1.f, n.Z + 1.f)
    | None ->
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
            let color = color r |> vector3ToColor
            yield (i, ny - 1 - j, color)
    }


[<EntryPoint>]
[<STAThread>]
let main args =
    Application.EnableVisualStyles ()

    let nx, ny = 800, 400

    use form = new Form (Width = nx, Height = ny, Text = "Eyedazzler")

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