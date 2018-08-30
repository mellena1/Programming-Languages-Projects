using System;
using System.Diagnostics;
using comp3350_lab8;

public class GC
{

    public static void Main(String[] args)
    {
        // change this section to run Part2 and Part 3
        // Part1 p = new Part1();
        // Part2 p = new Part2();
        Part3 p = new Part3();
        p.Run();
    }

    /* print out a tab separated counter and time value */
    public static void PrintTime(int tick, Stopwatch timer)
    {
        Console.Out.WriteLine(tick + "\t" + timer.ElapsedTicks);
    }
}
