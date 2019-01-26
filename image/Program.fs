open System.Drawing

[<EntryPoint>]
let main _ =
    let x, y = (1600, 800)
    use img = new Bitmap (x, y)
    Tracer.simpleCamera x y 100 |> Seq.iter img.SetPixel
    img.Save ("trace.png", Imaging.ImageFormat.Png)
    0
