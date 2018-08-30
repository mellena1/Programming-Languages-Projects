namespace A1.Tests.Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open A1.A1

[<TestClass>]
type TestClass () =

    [<TestMethod>]
    member this.Test1 () =
        Assert.IsTrue(true)

    [<TestMethod>]
    member this.C20_5 () =
        let actual = C 20 5
        let expected = bigint 15504
        Assert.AreEqual(expected, actual)

    [<TestMethod>]
    member this.CComplexity () =
        let actual = CComplexity ()
        Assert.AreNotEqual(NoAns, actual)

    [<TestMethod>]
    member this.Biggest () =
        let actual = biggest [1; 8; 12; 99; 2; -100]
        let expected = 99
        Assert.AreEqual(expected, actual)

    [<TestMethod>]
    member this.Positive () =
        let actual = positive [8; 6; -9; -1; 3; -4]
        let expected = [8; 6; 3]
        Assert.AreEqual(expected, actual)

    [<TestMethod>]
    member this.Intersect () =
        let actual = intersect [1; 5; 9; 4] [2; 5; 4; 10]
        let expected = [5; 4]
        Assert.AreEqual(expected, actual)

    [<TestMethod>]
    member this.Insert () =
        let actual = insert 5 [1; 4; 10]
        let expected = [1; 4; 5; 10]
        Assert.AreEqual(expected, actual)

    [<TestMethod>]
    member this.insertComplexity () =
        let actual = insertComplexity ()
        Assert.AreNotEqual(NoAns, actual)

    [<TestMethod>]
    member this.Sort () =
        let actual = sort [2; 10; 1; 9; 4]
        let expected = [1; 2; 4; 9; 10]
        Assert.AreEqual(expected, actual)

    [<TestMethod>]
    member this.sortComplexity () =
        let actual = sortComplexity ()
        Assert.AreNotEqual(NoAns, actual)

    [<TestMethod>]
    member this.Vecadd () =
        let actual = vecadd [1; 2; 3] [4; 5; 6]
        let expected = [5; 7; 9]
        Assert.AreEqual(expected, actual)

    [<TestMethod>]
    member this.Matadd () =
        let actual = matadd [ [1;2]; [3;4] ] [ [5;6]; [7;8] ]
        let expected = [ [6;8]; [10;12] ]
        Assert.AreEqual(expected, actual)
