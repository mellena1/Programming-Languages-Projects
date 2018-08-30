/* 
    COMP3350 lab3a - Prolog introduction
*/

/* insert your facts and rules here */

/* Facts */
quarterback(andy_dalton).
quarterback(aj_mccarron).
runningback(gio_bernard).
runningback(joe_mixon).
wide_receiver(aj_green).
wide_receiver(john_ross).
defensive_lineman(geno_atkins).
defensive_lineman(carlos_dunlap).
linebacker(vontaze_burfict).

/* Rules */
catches(X) :- wide_receiver(X); runningback(X).
throws_to(X, Y) :- quarterback(X), catches(Y).
offense(X) :- quarterback(X); runningback(X); wide_receiver(X).
defense(X) :- defensive_lineman(X); linebacker(X).
tackles(X, Y) :- defense(X), offense(Y).


/* put your example queries in this comment under your clauses 

?- quarterback(X).
?- throws_to(andy_dalton, Y).
?- tackles(geno_atkins, gio_bernard).
?- tackles(geno_atkins, carlos_dunlap).

*/
