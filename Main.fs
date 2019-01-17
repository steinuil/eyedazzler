open System
open System.Drawing
open System.Windows.Forms

[<EntryPoint>]
[<STAThread>]
let main _args =
    Application.EnableVisualStyles ()

    use form = new Form (Width = 200, Height = 200, Text = "Eyedazzler")

    let nx, ny = 100, 100

    use img = new Bitmap (nx, ny)

    form.Paint.Add (fun e ->
        for j = 0 to ny - 1 do
            for i = 0 to nx - 1 do
                let r = float i / float nx
                let g = float j / float ny
                let b = 0.2
                let color = Color.FromArgb
                                (255,
                                 int (255.99 * r),
                                 int (255.99 * g),
                                 int (255.99 * b))
                img.SetPixel(i, j, color)

        e.Graphics.DrawImage (img, 0, 0)
    )

    Application.Run form
    0