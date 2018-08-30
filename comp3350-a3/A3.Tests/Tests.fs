module Tests

open System
open Xunit
open A3.A3

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

[<Fact>]
let ``testUnionSI`` () =
    let i1 = I [1111;2222;3333]
    let i2 = I [5555; 6666]
    let s1 = S ["COMP"; "HIST"; "PHYS"]
    let s2 = S ["CHEM"; "MATH"]
    let si1 = product s1 i1
    let si2 = product s2 i2

    let actual = union si1 si2
    let expected = SI [("COMP", 1111); ("COMP", 2222); ("COMP", 3333); ("HIST", 1111); ("HIST", 2222); 
                       ("HIST", 3333); ("PHYS", 1111); ("PHYS", 2222); ("PHYS", 3333); ("CHEM", 5555);
                       ("CHEM", 6666); ("MATH", 5555); ("MATH", 6666)]
    Assert.Equal<SET>(expected, actual)

[<Fact>]
let ``testUnionSIIS`` () =
    let i1 = I [1111]
    let i2 = I [2222]
    let s1 = S ["COMP"]
    let s2 = S ["CHEM"]
    let is = product i1 s1
    let si1 = product s1 i1
    let si2 = product s2 i2
    let siis1 = product si1 is
    let siis2 = product si2 is

    let actual = union siis1 siis2
    let expected = SIIS [(("COMP", 1111), (1111, "COMP")); (("CHEM", 2222), (1111, "COMP"))]
    Assert.Equal<SET>(expected, actual)

[<Fact>]
let ``testgtSx1`` () =
    let actual = gtSx "red" ("blue", 5)
    let expected = false
    Assert.Equal(expected, actual)

[<Fact>]
let ``testgtSx2`` () =
    let actual = gtSx "blue" ("red", 5)
    let expected = true
    Assert.Equal(expected, actual)

[<Fact>]
let ``testeqxIxx1`` () =
    let actual = eqxIxx 4 (("red", 4), (6, "blue"))
    let expected = true
    Assert.Equal(expected, actual)

[<Fact>]
let ``testeqxIxx2`` () =
    let actual = eqxIxx 4 (("red", 3), (6, "blue"))
    let expected = false
    Assert.Equal(expected, actual)

[<Fact>]
let ``testFilterSF`` () =
    let actual = filter (gtSx "COMP") [("COMP", 3.0); ("PHYS", 4.0); ("MATH", 5.0)]
    let expected = [("PHYS", 4.0); ("MATH", 5.0)]
    Assert.Equal<(string * float) list>(expected, actual)

[<Fact>]
let ``testFilterSIIS`` () =
    let si1 = SI [("a", 1); ("b", 2)]
    let is1 = IS [(6, "c"); (7, "d"); (8, "e")]

    let (SIIS x) = product si1 is1

    let actual = SIIS (filter (eqxIxx 1) x)
    let expected = SIIS [(("a", 1), (6, "c")); (("a", 1), (7, "d")); (("a", 1), (8, "e"))]
    Assert.Equal<SET>(expected, actual)

[<Fact>]
let ``testSelectSI`` () =
    let i1 = I [1111;2222;3333]
    let s1 = S ["COMP"; "HIST"; "PHYS"]
    let si1 = product s1 i1

    let actual = selectSI si1 (gtSx "COMP")
    let expected = SI [("HIST", 1111); ("HIST", 2222); ("HIST", 3333); ("PHYS", 1111); ("PHYS", 2222); ("PHYS", 3333)]
    Assert.Equal<SET>(expected, actual)

[<Fact>]
let ``testSelectSIIS`` () =
    let i1 = I [1111;2222;3333]
    let s1 = S ["COMP"; "HIST"; "PHYS"]
    let si1 = product s1 i1
    let is = product i1 s1
    let siis1 = product si1 is

    let actual = selectSIIS siis1 (eqxIxx 2222)
    let expected = SIIS [(("COMP", 2222), (1111, "COMP")); (("COMP", 2222), (1111, "HIST"));
                         (("COMP", 2222), (1111, "PHYS")); (("COMP", 2222), (2222, "COMP"));
                         (("COMP", 2222), (2222, "HIST")); (("COMP", 2222), (2222, "PHYS"));
                         (("COMP", 2222), (3333, "COMP")); (("COMP", 2222), (3333, "HIST"));
                         (("COMP", 2222), (3333, "PHYS")); (("HIST", 2222), (1111, "COMP"));
                         (("HIST", 2222), (1111, "HIST")); (("HIST", 2222), (1111, "PHYS"));
                         (("HIST", 2222), (2222, "COMP")); (("HIST", 2222), (2222, "HIST"));
                         (("HIST", 2222), (2222, "PHYS")); (("HIST", 2222), (3333, "COMP"));
                         (("HIST", 2222), (3333, "HIST")); (("HIST", 2222), (3333, "PHYS"));
                         (("PHYS", 2222), (1111, "COMP")); (("PHYS", 2222), (1111, "HIST"));
                         (("PHYS", 2222), (1111, "PHYS")); (("PHYS", 2222), (2222, "COMP"));
                         (("PHYS", 2222), (2222, "HIST")); (("PHYS", 2222), (2222, "PHYS"));
                         (("PHYS", 2222), (3333, "COMP")); (("PHYS", 2222), (3333, "HIST"));
                         (("PHYS", 2222), (3333, "PHYS"))]
    Assert.Equal<SET>(expected, actual)

[<Fact>]
let ``testAnonComparatorsSI`` () =
    let i1 = I [1111;2222;3333]
    let s1 = S ["COMP"; "HIST"; "PHYS"]
    let si1 = product s1 i1

    let actual = selectSI si1 (fun (a, _) -> a > "COMP")
    let expected = SI [("HIST", 1111); ("HIST", 2222); ("HIST", 3333); ("PHYS", 1111); ("PHYS", 2222); ("PHYS", 3333)]
    Assert.Equal<SET>(expected, actual)

[<Fact>]
let ``testAnonComparatorsSIIS`` () =
    let i1 = I [1111;2222;3333]
    let s1 = S ["COMP"; "HIST"; "PHYS"]
    let si1 = product s1 i1
    let is = product i1 s1
    let siis1 = product si1 is

    let actual = selectSIIS siis1 (fun ((_, a), (_, _)) -> (a = 2222))
    let expected = SIIS [(("COMP", 2222), (1111, "COMP")); (("COMP", 2222), (1111, "HIST"));
                         (("COMP", 2222), (1111, "PHYS")); (("COMP", 2222), (2222, "COMP"));
                         (("COMP", 2222), (2222, "HIST")); (("COMP", 2222), (2222, "PHYS"));
                         (("COMP", 2222), (3333, "COMP")); (("COMP", 2222), (3333, "HIST"));
                         (("COMP", 2222), (3333, "PHYS")); (("HIST", 2222), (1111, "COMP"));
                         (("HIST", 2222), (1111, "HIST")); (("HIST", 2222), (1111, "PHYS"));
                         (("HIST", 2222), (2222, "COMP")); (("HIST", 2222), (2222, "HIST"));
                         (("HIST", 2222), (2222, "PHYS")); (("HIST", 2222), (3333, "COMP"));
                         (("HIST", 2222), (3333, "HIST")); (("HIST", 2222), (3333, "PHYS"));
                         (("PHYS", 2222), (1111, "COMP")); (("PHYS", 2222), (1111, "HIST"));
                         (("PHYS", 2222), (1111, "PHYS")); (("PHYS", 2222), (2222, "COMP"));
                         (("PHYS", 2222), (2222, "HIST")); (("PHYS", 2222), (2222, "PHYS"));
                         (("PHYS", 2222), (3333, "COMP")); (("PHYS", 2222), (3333, "HIST"));
                         (("PHYS", 2222), (3333, "PHYS"))]
    Assert.Equal<SET>(expected, actual)

[<Fact>]
let ``testDifferenceSI`` () =
    let i1 = I [1111; 2222; 3333]
    let i2 = I [1111; 5555; 6666]
    let s1 = S ["COMP"; "HIST"; "PHYS"]
    let s2 = S ["COMP"; "CHEM"; "MATH"]
    // Only overlap between si1 and si2 should be ("COMP", 1111)
    let si1 = product s1 i1
    let si2 = product s2 i2

    let actual = difference si1 si2
    let expected = SI [("COMP", 2222); ("COMP", 3333); ("HIST", 1111);
                       ("HIST", 2222); ("HIST", 3333); ("PHYS", 1111);
                       ("PHYS", 2222); ("PHYS", 3333)]
    Assert.Equal<SET>(expected, actual)

[<Fact>]
let ``testDifferenceSIIS`` () =
    let i1 = I [1111;2222;3333]
    let i2 = I [5555; 6666]
    let s1 = S ["COMP"; "HIST"; "PHYS"]
    let s2 = S ["CHEM"; "MATH"]
    let is = product i1 s1
    let si1 = product s1 i1
    let si2 = product s2 i2
    let siis1 = product si1 is
    let siis2 = product si2 is

    let actual = difference siis1 siis2
    let expected = SIIS     [(("COMP", 1111), (1111, "COMP")); (("COMP", 1111), (1111, "HIST"));
                             (("COMP", 1111), (1111, "PHYS")); (("COMP", 1111), (2222, "COMP"));
                             (("COMP", 1111), (2222, "HIST")); (("COMP", 1111), (2222, "PHYS"));
                             (("COMP", 1111), (3333, "COMP")); (("COMP", 1111), (3333, "HIST"));
                             (("COMP", 1111), (3333, "PHYS")); (("COMP", 2222), (1111, "COMP"));
                             (("COMP", 2222), (1111, "HIST")); (("COMP", 2222), (1111, "PHYS"));
                             (("COMP", 2222), (2222, "COMP")); (("COMP", 2222), (2222, "HIST"));
                             (("COMP", 2222), (2222, "PHYS")); (("COMP", 2222), (3333, "COMP"));
                             (("COMP", 2222), (3333, "HIST")); (("COMP", 2222), (3333, "PHYS"));
                             (("COMP", 3333), (1111, "COMP")); (("COMP", 3333), (1111, "HIST"));
                             (("COMP", 3333), (1111, "PHYS")); (("COMP", 3333), (2222, "COMP"));
                             (("COMP", 3333), (2222, "HIST")); (("COMP", 3333), (2222, "PHYS"));
                             (("COMP", 3333), (3333, "COMP")); (("COMP", 3333), (3333, "HIST"));
                             (("COMP", 3333), (3333, "PHYS")); (("HIST", 1111), (1111, "COMP"));
                             (("HIST", 1111), (1111, "HIST")); (("HIST", 1111), (1111, "PHYS"));
                             (("HIST", 1111), (2222, "COMP")); (("HIST", 1111), (2222, "HIST"));
                             (("HIST", 1111), (2222, "PHYS")); (("HIST", 1111), (3333, "COMP"));
                             (("HIST", 1111), (3333, "HIST")); (("HIST", 1111), (3333, "PHYS"));
                             (("HIST", 2222), (1111, "COMP")); (("HIST", 2222), (1111, "HIST"));
                             (("HIST", 2222), (1111, "PHYS")); (("HIST", 2222), (2222, "COMP"));
                             (("HIST", 2222), (2222, "HIST")); (("HIST", 2222), (2222, "PHYS"));
                             (("HIST", 2222), (3333, "COMP")); (("HIST", 2222), (3333, "HIST"));
                             (("HIST", 2222), (3333, "PHYS")); (("HIST", 3333), (1111, "COMP"));
                             (("HIST", 3333), (1111, "HIST")); (("HIST", 3333), (1111, "PHYS"));
                             (("HIST", 3333), (2222, "COMP")); (("HIST", 3333), (2222, "HIST"));
                             (("HIST", 3333), (2222, "PHYS")); (("HIST", 3333), (3333, "COMP"));
                             (("HIST", 3333), (3333, "HIST")); (("HIST", 3333), (3333, "PHYS"));
                             (("PHYS", 1111), (1111, "COMP")); (("PHYS", 1111), (1111, "HIST"));
                             (("PHYS", 1111), (1111, "PHYS")); (("PHYS", 1111), (2222, "COMP"));
                             (("PHYS", 1111), (2222, "HIST")); (("PHYS", 1111), (2222, "PHYS"));
                             (("PHYS", 1111), (3333, "COMP")); (("PHYS", 1111), (3333, "HIST"));
                             (("PHYS", 1111), (3333, "PHYS")); (("PHYS", 2222), (1111, "COMP"));
                             (("PHYS", 2222), (1111, "HIST")); (("PHYS", 2222), (1111, "PHYS"));
                             (("PHYS", 2222), (2222, "COMP")); (("PHYS", 2222), (2222, "HIST"));
                             (("PHYS", 2222), (2222, "PHYS")); (("PHYS", 2222), (3333, "COMP"));
                             (("PHYS", 2222), (3333, "HIST")); (("PHYS", 2222), (3333, "PHYS"));
                             (("PHYS", 3333), (1111, "COMP")); (("PHYS", 3333), (1111, "HIST"));
                             (("PHYS", 3333), (1111, "PHYS")); (("PHYS", 3333), (2222, "COMP"));
                             (("PHYS", 3333), (2222, "HIST")); (("PHYS", 3333), (2222, "PHYS"));
                             (("PHYS", 3333), (3333, "COMP")); (("PHYS", 3333), (3333, "HIST"));
                             (("PHYS", 3333), (3333, "PHYS"))]
    Assert.Equal<SET>(expected, actual)
