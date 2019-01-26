namespace Camera

open System.Numerics
open Ray


type Camera =
    { lowerLeft : Vector3
      horizontal : Vector3
      vertical : Vector3
      origin : Vector3 }


module Camera =
    let Default =
        { lowerLeft = Vector3 (-2.f, -1.f, -1.f)
          horizontal = Vector3 (4.f, 0.f, 0.f)
          vertical = Vector3 (0.f, 2.f, 0.f)
          origin = Vector3 (0.f, 0.f, 0.f) }

    let getRay cam (x : float32) (y : float32) =
        { origin = cam.origin
          direction = cam.lowerLeft + x * cam.horizontal + y * cam.vertical - cam.origin }
