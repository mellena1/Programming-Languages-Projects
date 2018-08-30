namespace LAB4

module LAB4 =
    
    let oddSquares numTerms =
        let rec oddList a numTerms =
            // Function to create a double list of size numTerms
            // of the odd numbers starting from a
            match numTerms with
                | 0 -> []
                | _ -> (double a)::(oddList (a+2) (numTerms-1))
        // Return a list of x^2 odd numbers
        List.map (fun x -> x*x) (oddList 1 numTerms)

    // define the function using a List.foldBack call
    let piApprox numTerms =
        let continuedFrac = fun a b -> 6.0 + (a / b)
        (List.foldBack continuedFrac (oddSquares numTerms) 1.0) - 3.0
    
    // define the function using a List.foldBack call
    let eApprox numTerms =
        let k = [1.0..(float numTerms)]
        let continuedFrac = fun a b -> a + (a / b)
        2.0 + (1.0 / (List.foldBack continuedFrac k 1.0))
