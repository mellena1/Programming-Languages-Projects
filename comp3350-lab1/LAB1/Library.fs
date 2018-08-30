namespace LAB1

module LAB1 =

    let hello name =
        printfn "Hello %s" name

    let rec GCD x y =
        if y = 0 then x else GCD y (x % y)

    let rec factorial (n: int):bigint =
        if n <= 1 then bigint n else bigint n * factorial (n-1)

    [<EntryPoint>]
    let main args =
        printf "hello function: "
        hello "world!"
        printf "GCD function: %d\n" (GCD 9 18)
        printf "factorial function: %A\n" (factorial 20)
        0
