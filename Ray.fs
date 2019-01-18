namespace Ray

open System.Numerics

type Ray =
    { origin : Vector3
      direction : Vector3 }

module Ray =
    let pointAt (t : float32) ray =
        ray.origin + t * ray.direction