namespace A4

module A4 =
  type 'element mylist = NIL | CONS of 'element * 'element mylist
  type Complexity = NoAns | Constant | Linear | Logarithmic | NLogN | NSquared | NFactorial


  let rec helper a b =
    match a with
      | CONS (h, NIL) -> CONS (h, b)
      | CONS (h, t) -> helper t (CONS (h, b))
  
  // implement tail recursive reverse function by calling the helper function
  let rev L =
    helper L NIL

  let rec helperRevList a b =
    match a with
      | [] -> b
      | h::t -> helperRevList t (h::b)

  let revList L = helperRevList L []

  let rec vecAddHelper v1 v2 c =
    match (v1, v2) with
      | ([], []) -> c
      | (h1::t1, h2::t2) -> vecAddHelper t1 t2 ((h1+h2)::c)
  
  let vecadd v1 v2 =
    revList (vecAddHelper v1 v2 [])
  
  let vecaddComplexity = Linear
  // vecadd is linear (O(n)) because with tail recurision it only has to call
  // vecAddHelper once for each element. It then has to call revList, which is
  // also O(n). So technically this is a O(2n) function, but that reduces down
  // to O(n).

  // modify the function signature and function itself to allow for a comparison function parameter
  // mergeSort currently sorts in ascending order
  let rec mergeSort comp L =
    match L with
      | [] -> []
      | _::[] -> L
      | theList ->
        let rec halve L =
          match L with
            | [] -> ([], [])
            | a::[] -> ([a], [])
            | a::b::cs ->
              let (x, y) = halve cs
              (a::x, b::y)
        let rec merge (L1, L2) =
          match (L1, L2) with
            | ([], ys) -> ys
            | (xs, []) -> xs
            | (x::xs, y::ys) when comp x y -> x::merge(xs,y::ys)
            | (x::xs, y::ys) -> y::merge(x::xs,ys)
        let (x, y) = halve theList
        in
        merge (mergeSort comp x, mergeSort comp y)


  // modify your function from above to work with mylist type
  let rec mylistMergeSort comp L =
    match L with
      | NIL -> NIL
      | CONS (_, NIL) -> L
      | theList ->
        let rec halve L =
          match L with
            | NIL -> (NIL, NIL)
            | CONS (a, NIL) -> (CONS(a, NIL), NIL)
            | CONS (a, CONS(b, cs)) ->
              let (x, y) = halve cs
              (CONS(a, x), CONS(b, y))
        let rec merge (L1, L2) =
          match (L1, L2) with
            | (NIL, ys) -> ys
            | (xs, NIL) -> xs
            | (CONS(x, xs), CONS(y, ys)) when comp x y -> CONS(x, merge(xs,CONS(y, ys)))
            | (CONS(x, xs), CONS(y,ys)) -> CONS(y, merge(CONS(x, xs),ys))
        let (x, y) = halve theList
        in
        merge (mylistMergeSort comp x, mylistMergeSort comp y)
  