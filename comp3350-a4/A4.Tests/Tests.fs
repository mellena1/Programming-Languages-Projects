module Tests

open System
open Xunit
open A4.A4

[<Fact>]
let ``testRev`` () =
    let c = CONS (1, CONS (2, CONS (3, NIL)))
    let actual = rev c
    let expected = CONS (3, CONS (2, CONS (1, NIL)))
    Assert.Equal(expected, actual)

[<Fact>]
let ``testVecAdd`` () =
    let actual = vecadd [1;2;3] [4;5;6]
    let expected = [5;7;9]
    Assert.Equal<int list>(expected, actual)

[<Fact>]
let ``testMergeSort`` () =
    let actual = mergeSort (fun a b -> a < b) [2;1;3;5;4]
    let expected = [1;2;3;4;5]
    Assert.Equal<int list>(expected, actual)
    let actual = mergeSort (fun a b -> a > b) [2;1;3;5;4]
    let expected = [5;4;3;2;1]
    Assert.Equal<int list>(expected, actual)

[<Fact>]
let ``testmylistMergeSort`` () =
    let c = (CONS (6, CONS (12, CONS (3, NIL))))
    let actual = mylistMergeSort (fun a b -> a > b) c
    let expected = CONS(12,CONS(6,CONS(3,NIL)))
    Assert.Equal(expected, actual)
    let actual = mylistMergeSort (fun a b -> a < b) c
    let expected = CONS(3,CONS(6,CONS(12,NIL)))
    Assert.Equal(expected, actual)
