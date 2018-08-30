/* 
    COMP3350 lab7b - Prolog introduction
    trains!
*/

station(hamburg).
station(bremen).
station(hannover).
station(berlin).
station(leipzig).
station(osnabruck).
station(stuttgart).
station(fulda).
station(munich).

adjacent(hamburg, bremen).
adjacent(hamburg, berlin).
adjacent(berlin, hannover).
adjacent(berlin, leipzig).
adjacent(leipzig, fulda).
adjacent(fulda, hannover).
adjacent(hannover, osnabruck).
adjacent(osnabruck, bremen).
adjacent(stuttgart, munich).

/* insert your clauses here */

isAdjacent(X, Y) :- adjacent(X, Y); adjacent(Y, X).
connected(X, Y) :- isAdjacent(X, Y), !; isAdjacent(X, Z), connected(Z, Y), !.
