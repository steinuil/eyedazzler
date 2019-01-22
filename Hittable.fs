namespace Hittable

open System.Numerics
open Ray

type HitRecord =
    { t : float32
      p : Vector3
      normal : Vector3 }

type IHittable =
    abstract member Hits : Ray
        -> tMin:float32
        -> tMax:float32
        -> HitRecord option