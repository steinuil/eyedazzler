namespace Hittable

open System.Numerics
open Ray


type HitRecord =
    { t : float32
      p : Vector3
      normal : Vector3 }


type IHittable =
    abstract member Hits : Ray -> float32 -> float32 -> HitRecord option

    
module Hittable =
    let hits (t : IHittable) (ray : Ray) tMin tMax =
        t.Hits ray tMin tMax


type HittableList =
    { list : IHittable list }

    interface IHittable with
        member this.Hits (ray : Ray) tMin tMax =
            let hr, _ =
                List.foldBack (fun (item : IHittable) (rcrd, closest) ->
                    match item.Hits ray tMin closest with
                    | None -> (rcrd, closest)
                    | Some hr ->
                        (Some hr, hr.t)
                ) this.list (None, tMax)
            hr