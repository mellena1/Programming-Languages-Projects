namespace A1

module A1 =
    type Complexity = NoAns | Constant | Linear | Logarithmic | NLogN | NSquared | NFactorial

    let rec factorial (n: int):bigint =
        if n <= 1 then bigint n else bigint n * factorial (n-1)

    let C n k =
        factorial n / ((factorial k) * factorial (n - k))

    let CComplexity () = Linear
        // The complexity of C can be described as O(n+k), because of having
        // to do the factorial of both n and k."

    let rec biggest (L : int list) =
        if L.Tail.IsEmpty then L.Head
        elif L.Head > L.Tail.Head then biggest (L.Head::L.Tail.Tail)
        else biggest L.Tail

    let rec positive (L : int list) =
        if L.IsEmpty then L
        elif L.Head > 0 then L.Head::positive L.Tail
        else positive L.Tail

    let rec isMember (a: 'a) (L: 'a List) =
        match L with
            | [] -> false
            | h::t -> if a=h then true else isMember a t

    let rec intersect (L1: 'a List) (L2: 'a List) =
        if L1.IsEmpty then L1
        elif isMember L1.Head L2 then L1.Head::(intersect L1.Tail L2)
        else intersect L1.Tail L2

    let rec insert (e: int) (L:int list) =
        if L.IsEmpty then [e]
        elif e < L.Head then e::L
        else L.Head::(insert e L.Tail)

    let insertComplexity () = Linear
        // The complexity of insert can be described as O(n), because it looks
        // through each element to see if e is smaller than it. If it gets to
        // the end of the list, it knows e must go there. So it only checks
        // against each element once."

    let rec sort (L:int List) =
        if L.IsEmpty then [] else insert L.Head (sort L.Tail)

    let sortComplexity () =NSquared
        // The complexity of sort can be described as O(n^2), because you need 
        // to iterate through every value of the list (O(n)) and then insert it,
        // which is also O(n)."

    let rec vecadd (L1: int List) (L2: int List) = 
        if L1.IsEmpty then []
        else (L1.Head + L2.Head)::vecadd L1.Tail L2.Tail

    let rec matadd (L1: int List List) (L2: int List List) =
        if L1.IsEmpty then []
        else (vecadd L1.Head L2.Head)::(matadd L1.Tail L2.Tail)
