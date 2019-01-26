namespace Vec2
open System.Drawing

/// Unused Vec2 type for learning vectors.
type Vec2 =
    { x : float32
      y : float32 }


module Vec2 =
    let add v1 v2 =
        { x = v1.x + v2.x
          y = v1.y + v2.y }

    let scale v scalar =
        { x = v.x * scalar
          y = v.y * scalar }

    let scaleDiv v scalar =
        { x = v.x / scalar
          y = v.y / scalar }

    let length v =
        v.x * v.x + v.y * v.y |> sqrt

    let toUnit v =
        scaleDiv v (length v)

    let draw v origin =
        let p1 = new PointF (origin.x, -origin.y)
        let v = add origin v
        let p2 = new PointF (v.x, -v.y)
        (p1, p2)

    let ( + ) = add