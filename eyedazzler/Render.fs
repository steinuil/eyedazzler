module Render
open System.Numerics
open System.Drawing


/// Yields coordinates from (0, y) to (x, 0)
let pixels x y =
    seq {
        for j = y - 1 downto 0 do
            for i = 0 to x - 1 do
                yield (i, j)
    }


let vec3ToColor (vec : Vector3) =
    Color.FromArgb
        (255,
         int (255.99f * vec.X),
         int (255.99f * vec.Y),
         int (255.99f * vec.Z))