namespace LAB2

module LAB2 =

    
    // Note: you need to change this function signature to use patterns
    let addTuples a b =
        match (a,b) with
            | ((a1, a2), (b1, b2)) -> (a1+b1, a2+b2)

    let dup f x =
        f(f x)
