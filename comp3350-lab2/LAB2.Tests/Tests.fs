module Tests

open System
open Xunit
open LAB2.LAB2

[<Fact>]
let ``YouRanTheTests`` () =
    Assert.True(true);

[<Fact>]
let ``testAdd`` () =
    let actual = addTuples (3, 4) (5, 6)
    let expected = (8, 10)
    Assert.Equal(expected, actual)

[<Fact>]
let ``testDupInc`` () =
    let inc (x: int) = x + 1
    let actual = dup inc 5
    let expected = 7
    Assert.Equal(expected, actual)

[<Fact>]
let ``testDupTail`` () =
    let tail (L: 'a list) = L.Tail
    let actual = dup tail [1;2;3;4;5]
    let expected = [3;4;5]
    Assert.Equal<int list>(expected, actual)
