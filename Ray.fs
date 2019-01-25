namespace Ray

open System.Numerics

type Ray =
    { origin : Vector3
      direction : Vector3 }

module Ray =
    /// Make ray with unit length direction
    let make origin (direction : Vector3) =
        { origin = origin
          direction = Vector3.Normalize direction }

    /// Get the point at position <c>t</c> along the ray
    let pointAt (t : float32) ray =
        ray.origin + t * ray.direction