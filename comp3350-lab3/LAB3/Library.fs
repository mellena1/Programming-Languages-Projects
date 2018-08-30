namespace LAB3

module LAB3 =
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
