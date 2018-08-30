using System;
using System.Diagnostics;

namespace comp3350_lab8
{
    public class Part2
    {
        Stopwatch timer;

        public Part2() {
            
        }

        /* Write your code in this method */
        public void Run()
        {
            timer = Stopwatch.StartNew();
            GC.PrintTime(0, timer);
            LargeObject[] lo = new LargeObject[10000];
            for (int i = 1; i <= 10000; i++) {
                lo[i-1] = new LargeObject();
                if (i % 1000 == 0)
                    System.GC.Collect(0);
                GC.PrintTime(i, timer);
            }
        }
    }
}
