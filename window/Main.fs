open System
open System.Drawing
open System.Windows.Forms


[<EntryPoint>]
[<STAThread>]
let main _ =
    Application.EnableVisualStyles ()
    Application.ThreadException.Add (fun exn ->
        Windows.MessageBox.Show exn.Exception.Message |> ignore
    )

    let nx, ny = 800, 400

    use form = new Form (Text = "Eyedazzler")
    use box = new PictureBox ()
    form.AutoSize <- true
    box.Size <- new Size (nx, ny)

    use img = new Bitmap (nx, ny)
    Tracer.simpleCamera nx ny 200 |> Seq.iter img.SetPixel

    box.Image <- img
    form.Controls.Add box
    Application.Run form
    0