module Tests

open System
open Xunit
open A2.A2

[<Fact>]
let ``YouRanTheTests`` () =
    Assert.True(true);

[<Fact>]
let ``testThird`` () =
    let actual = third [1;2;3;1;2;3;1;2;3;1;2;3]
    let expected = ([1;1;1;1], [2;2;2;2], [3;3;3;3])
    Assert.Equal(expected, actual)

[<Fact>]
let ``testThirdComplexity`` () =
    let actual = thirdComplexity
    Assert.NotEqual(NoAns, actual)

[<Fact>]
let ``testCompIncInc`` () =
    let inc (x: int) = x + 1
    let actual = comp inc inc 5
    let expected = 7
    Assert.Equal(expected, actual)

[<Fact>]
let ``testSqList`` () =
    let actual = sqlist [1;2;3;4]
    let expected = [1;4;9;16]
    Assert.Equal<int list>(expected, actual)

[<Fact>]
let ``testSqListComplexity`` () =
    let actual = sqlistComplexity
    Assert.NotEqual(NoAns, actual)

[<Fact>]
let ``testVecAdd`` () =
    let actual = vecadd [1;2;3] [4;5;6]
    let expected = [5;7;9]
    Assert.Equal<int list>(expected, actual)

[<Fact>]
let ``testMatAdd`` () =
    let actual = matadd [ [1;2]; [3;4] ] [ [5;6]; [7;8] ]
    let expected = [ [6;8]; [10;12] ]
    Assert.Equal<int list>(expected, actual)

[<Fact>]
let ``testInnerProduct`` () =
    let actual = ip [1;2;3] [1;2;3]
    let expected = 14
    Assert.Equal(expected, actual)
