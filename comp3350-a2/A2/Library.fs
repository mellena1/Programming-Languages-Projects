namespace A2

module A2 =
    type Complexity = NoAns | Constant | Linear | Logarithmic | NLogN | NSquared | NFactorial

    let rec third L =
        match L with
            | [] -> ([], [], [])
            | [a] -> ([a], [], [])
            | a::[b] -> ([a], [b], [])
            | a::b::c::t -> 
              let (x, y, z) = third(t)
              in
              (a::x, b::y, c::z)

    let thirdComplexity = Linear
    // third looks through every element in the list once. Although it steps
    // by 3 every time, making the complexity O(n/3), that can be simplified
    // to O(n).

    let comp f1 f2 x =
        f2 (f1 x)

    let sq x = x * x
    let rec map f L =
        match L with
          | [] -> []
          | h::t -> f h::map f t

    let sqlist L =
        map sq L

    let sqlistComplexity = Linear
    // sqlist makes a call to map. map runs once against each element in the
    // list. On that iteration, map calls sq, which is a O(1) operation. This
    // results in an overall complexity of O(n).

    let add x y = x + y

    let rec map2 f L1 L2 =
       match (L1,L2) with
          | ([], []) -> []
          | (h1::t1, h2::t2) -> f h1 h2::map2 f t1 t2
          | _ -> []  // Make the compiler happy

    let vecadd L1 L2 =
        map2 add L1 L2

    let matadd L1 L2 =
        map2 vecadd L1 L2
    
    let rec reduce f a L =
        match L with
          | [] -> a
          | h::t -> f h (reduce f a t)

    let mul x y = x * y

    let ip L1 L2 =
        reduce add 0 (map2 mul L1 L2)
    