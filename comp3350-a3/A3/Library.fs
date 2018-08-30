namespace A3

module A3 =
    type SET =
       | I of int list                                  // I [1;2;3]
       | S of string list                               // S ["a";"b";"c"]
       | IS of (int * string) list                      // IS [(1, "a");(2, "b")]
       | II of (int * int) list                         // II [(1,2); (3,4); (5,6)]
       | SS of (string * string) list                   // SS [("a","b"); ("c","d")]
       | SI of (string * int) list                      // SI [("a", 1); ("b", 2); ("c", 3)]
       | SISI of ((string * int) * (string * int)) list // SISI [(("a", 1), ("b", 2)); (("c", 3), "d", 4))]
       | SIIS of ((string * int) * (int * string)) list // SIIS [(("a", 1), (2, "b")); (("c", 3), (4, "d"))]

    let rec dist a s1 =
        match s1 with
          | [] -> []
          | _ -> (a, s1.Head)::(dist a s1.Tail)

    // Pairs generates the cartesian product
    let rec pairs s1 s2 =
        match (s1, s2) with
          | ([], _) -> []
          | (a, b) -> (dist a.Head b)@(pairs a.Tail b)

    let product s1 s2 =
      match (s1, s2) with
        | (I s1, I s2) -> II (pairs s1 s2)
        | (S s1, S s2) -> SS (pairs s1 s2)
        | (I s1, S s2) -> IS (pairs s1 s2)
        | (S s1, I s2) -> SI (pairs s1 s2)
        | (SI s1, SI s2) -> SISI (pairs s1 s2)
        | (SI s1, IS s2) -> SIIS (pairs s1 s2)
    
    let rec isMember a L =
      match L with
        | [] -> false
        | h::t when a=h -> true
        | h::t -> isMember a t

    let rec unionList l1 l2 =
      match l1 with
        | [] -> l2
        | h::t when not (isMember h l2) -> h::unionList t l2
        | h::t -> unionList t l2

    let union s1 s2 =
      match (s1, s2) with
        | (I l1, I l2) -> I (unionList l1 l2)
        | (IS l1, IS l2) -> IS (unionList l1 l2)
        | (SI l1, SI l2) -> SI (unionList l1 l2)
        | (SISI l1, SISI l2) -> SISI (unionList l1 l2)
        | (SIIS l1, SIIS l2) -> SIIS (unionList l1 l2)
    
    let gtIx x (a, _) = a > x
    let gtxxxI x ((_, _), (_, a)) = a > x
    
    let gtSx x (a, _) = (a > x)
    let eqxIxx x ((_, a), (_, _)) = (a = x)

    let rec filter f L =
        match L with
          | [] -> []
          | h::t when f h -> h::filter f t
          | h::t -> filter f t

    // 2.2.3 filter - tests in A3.Tests starting at line 120

    let selectIS s f =
        match s with
          | IS i -> IS (filter f i)

    let selectSISI s f =
        match s with
          | SISI i -> SISI (filter f i)

    let selectSI s f =
      match s with
        | SI i -> SI (filter f i)

    let selectSIIS s f =
      match s with
        | SIIS i -> SIIS (filter f i)

    // 2.2.5 Anonymous comparators in A3.Tests starting at line 172

    let rec differenceList l1 l2 =
      match l1 with
        | [] -> []
        | h::t when not (isMember h l2) -> h::differenceList t l2
        | h::t -> differenceList t l2

    // You should match a pattern with SI and SIIS and compute the difference of the sets
    let difference s1 s2 =
      match (s1, s2) with
        | (SI l1, SI l2) -> SI (differenceList l1 l2)
        | (SIIS l1, SIIS l2) -> SIIS (differenceList l1 l2)
