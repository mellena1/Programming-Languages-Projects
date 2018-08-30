module Tests

open Xunit
open LAB4.LAB4

[<Fact>]
let ``testOddSquares`` () =
    let actual = oddSquares 7
    let expected = [1.0;9.0;25.0;49.0;81.0;121.0;169.0]
    Assert.Equal<double>(expected, actual)

[<Fact>]
let ``testPiApprox`` () =
    let actual = piApprox 10
    Assert.InRange(actual, 3.141, 3.143)

[<Fact>]
let ``testeApprox`` () =
    let actual = eApprox 10
    Assert.InRange(actual, 2.717, 2.719)
