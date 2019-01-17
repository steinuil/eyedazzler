module Ray

open System.Numerics

type Ray =
    { origin: Vector3
      direction: Vector3 }

let pointAt (t : float32) ray =
    { ray with direction = t * ray.direction }