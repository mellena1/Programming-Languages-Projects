using System;
using System.Diagnostics;

namespace comp3350_lab8
{
    public class Part1
    {
        Stopwatch timer;

        public Part1() {
            
        }

        /* Write your code in this method */
        public void Run()
        {
            timer = Stopwatch.StartNew();
            GC.PrintTime(0, timer);
            LargeObject lo;
            for (int i = 1; i <= 10000; i++) {
                lo = new LargeObject();
                if (i % 1000 == 0)
                    System.GC.Collect();
                GC.PrintTime(i, timer);
            }
        }
    }
}
