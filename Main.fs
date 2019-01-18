open System
open System.Drawing
open System.Windows.Forms

open System.Numerics

let unitVector (vec : Vector3) =
    vec / vec.Length()

let color (r: Ray.Ray) =
    let unitDir = unitVector r.direction
    let t = 0.5f * (unitDir.Y + 1.0f)
    (1.0f - t) * Vector3 (1.0f, 1.0f, 1.0f) + t * Vector3 (0.5f, 0.7f, 1.0f)

[<EntryPoint>]
[<STAThread>]
let main _args =
    Application.EnableVisualStyles ()

    use form = new Form (Width = 400, Height = 200, Text = "Eyedazzler")

    let nx, ny = 400, 200

    let lowerLeft = Vector3 (-2.0f, -1.0f, -1.0f)
    let horizontal = Vector3 (4.0f, 0.0f, 0.0f)
    let vertical = Vector3 (0.0f, 2.0f, 0.0f)
    let origin = Vector3 (0.0f, 0.0f, 0.0f)

    use img = new Bitmap (nx, ny)

    form.Paint.Add (fun e ->
        for j = 0 to ny - 1 do
            for i = 0 to nx - 1 do
                let u = float32 i / float32 nx
                let v = float32 j / float32 ny

                let r : Ray.Ray =
                    { origin = origin;
                      direction = lowerLeft + (u * horizontal) + (v * vertical) }
                let col = color r

                let color = Color.FromArgb
                                (255,
                                 int (255.99f * col.X),
                                 int (255.99f * col.Y),
                                 int (255.99f * col.Z))
                img.SetPixel(i, j, color)

        e.Graphics.DrawImage (img, 0, 0)
    )

    Application.Run form
    0