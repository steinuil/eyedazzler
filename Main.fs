open System
open System.Drawing
open System.Windows.Forms

[<EntryPoint>]
[<STAThread>]
let main _args =
    Application.EnableVisualStyles ()

    use form = new Form (Width = 200, Height = 200, Text = "Eyedazzler")
    // use ctx = form.CreateGraphics ()

    // use brush = new SolidBrush (Color.Red)
    // let rect = new Rectangle (0, 0, 50, 50)

    // ctx.FillRectangle (brush, rect)

    Application.Run form
    0