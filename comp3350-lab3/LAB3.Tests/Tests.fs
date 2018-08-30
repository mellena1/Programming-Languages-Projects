module Tests

open System
open Xunit
open LAB3.LAB3

[<Fact>]
let ``testDist`` () =
    let actual = dist 1 ["a"; "b"; "c"]
    let expected = [(1, "a"); (1, "b"); (1, "c")]
    Assert.Equal<(int * string) List>(expected, actual)

[<Fact>]
let ``testPairs`` () =
    let actual = pairs [1; 2] ["a"; "b"; "c"]
    let expected = [(1, "a"); (1, "b"); (1, "c"); (2, "a"); (2, "b"); (2, "c")]
    Assert.Equal<(int * string) List>(expected, actual)

[<Fact>]
let ``testProductInts`` () =
    let i1 = I [1111;2222;3333]
    let i2 = I [5555;6666]

    let actual = product i1 i2
    let expected = II [(1111, 5555); (1111, 6666); (2222, 5555); (2222, 6666); (3333, 5555); (3333, 6666)]
    Assert.Equal<SET>(expected, actual)

[<Fact>]
let ``testProductIntString`` () =
    let i1 = I [1111;2222;3333]
    let s1 = S ["COMP"; "HIST"; "PHYS"]

    let actual = product i1 s1
    let expected = IS[(1111, "COMP"); (1111, "HIST"); (1111, "PHYS"); (2222, "COMP"); (2222, "HIST"); (2222, "PHYS"); (3333, "COMP"); (3333, "HIST"); (3333, "PHYS")]
    Assert.Equal<SET>(expected, actual)

[<Fact>]
let ``testProductStringInt`` () =
    let s2 = S ["CHEM"; "MATH"]
    let i2 = I [5555; 6666]

    let actual = product s2 i2
    let expected = SI [("CHEM", 5555); ("CHEM", 6666); ("MATH", 5555); ("MATH", 6666)]
    Assert.Equal<SET>(expected, actual)

[<Fact>]
let ``testProductSISI`` () =
    let si1 = SI [("a", 1); ("b", 2)]
    let si2 = SI [("c", 6); ("d", 7); ("e", 8)]

    let actual = product si1 si2
    let expected = SISI [(("a", 1), ("c", 6)); (("a", 1), ("d", 7)); (("a", 1), ("e", 8)); (("b", 2), ("c", 6)); (("b", 2), ("d", 7)); (("b", 2), ("e", 8))]
    Assert.Equal<SET>(expected, actual)

[<Fact>]
let ``testProductSIIS`` () =
    let si1 = SI [("a", 1); ("b", 2)]
    let is1 = IS [(6, "c"); (7, "d"); (8, "e")]

    let actual = product si1 is1
    let expected = SIIS [(("a", 1), (6, "c")); (("a", 1), (7, "d")); (("a", 1), (8, "e")); (("b", 2), (6, "c")); (("b", 2), (7, "d")); (("b", 2), (8, "e"))]
    Assert.Equal<SET>(expected, actual)
