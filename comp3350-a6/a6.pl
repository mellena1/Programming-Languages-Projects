% COMP3350 - a6

when_(1000,10).
when_(1200,12).
when_(3400,11).
when_(3350,12).
when_(2350,11).
where(1000,dobbs102).
where(1200,dobbs118).
where(3400,wentw216).
where(3350,wentw118).
where(2350,wentw216).
enroll(mary,1200).
enroll(john,3400).
enroll(mary,3350).
enroll(john,1000).
enroll(jim,1000).

/*******************************************
** define your four predicates below here **
**       leave this comment intact        **
*******************************************/
schedule(S, P, T) :- enroll(S, N), where(N, P), when_(N, T).

usage(P, T) :- where(N, P), when_(N, T).

roomconflict(N1, N2) :- 
    where(N1, P1), where(N2, P2), (P1 = P2), 
    when_(N1, T1), when_(N2, T2), (T1 = T2),
    (N1 \= N2).

meet(S1, S2) :- 
    schedule(S1, Ps1, Ts1),
    (schedule(S2, Ps1, Ts1); schedule(S2, Ps1, Ts1 - 1); schedule(S2, Ps1, Ts1 + 1)),
    (S1 \= S2).
