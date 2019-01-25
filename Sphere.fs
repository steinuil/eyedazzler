namespace Sphere

open System.Numerics
open Hittable
open Ray

type Sphere =
    val center : Vector3
    val radius : float32

    new (center : Vector3, radius : float32) =
        { center = center; radius = radius }

    // interface IHittable with
    //     member this.Hits ray tMin tMax =
    //         let oc = ray.origin - this.center
    //         let a = Vector3.Dot (ray.direction, ray.direction)
    //         let b = Vector3.Dot (oc, ray.direction)
    //         let c = Vector3.Dot (oc, oc)
    //         let discriminant = b * b - a * c
    //         if discriminant <= 0.f then None else
    //         let temp =
    //             let t = (-b - sqrt discriminant) / a
    //             if temp > tMin && temp < tMax then
    //                 temp
    //             else