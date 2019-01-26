namespace Sphere

open System.Numerics
open Hittable
open Ray


type Sphere =
    { center : Vector3
      radius : float32 }

    static member Make (center : Vector3) (radius : float32) =
        { center = center; radius = radius }

    interface IHittable with
        member sphere.Hits ray tMin tMax =
            let oc = ray.origin - sphere.center
            let a = Vector3.Dot (ray.direction, ray.direction)
            let b = Vector3.Dot (oc, ray.direction)
            let c = Vector3.Dot (oc, oc) - sphere.radius * sphere.radius
            let discriminant = b * b - a * c
            if discriminant <= 0.f then None else
            let temp =
                let t = (-b - sqrt discriminant) / a
                if t > tMin && t < tMax then Some t else
                let t = (-b + sqrt discriminant) / a
                if t > tMin && t < tMax then Some t else
                None
            temp |> Option.map (fun temp ->
                let p = Ray.pointAt temp ray
                { t = temp
                  p = p
                  normal = (p - sphere.center) / sphere.radius }
            )
