open System
open System.Drawing
open System.Windows.Forms

open System.Numerics

[<EntryPoint>]
[<STAThread>]
let main _args =
    Application.EnableVisualStyles ()

    use form = new Form (Width = 400, Height = 200, Text = "Eyedazzler")

    let nx, ny = 400, 200

    use img = new Bitmap (nx, ny)

    form.Paint.Add (fun e ->
        for j = 0 to ny - 1 do
            for i = 0 to nx - 1 do
                let vec = Vector3 (float32 i / float32 nx, float32 j / float32 ny, 0.2f)
                let color = Color.FromArgb
                                (255,
                                 int (255.99f * vec.X),
                                 int (255.99f * vec.Y),
                                 int (255.99f * vec.Z))
                img.SetPixel(i, j, color)

        e.Graphics.DrawImage (img, 0, 0)
    )

    Application.Run form
    0