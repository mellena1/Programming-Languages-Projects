using System;
using System.Diagnostics;

namespace comp3350_lab8
{
    public class Part3
    {
        Stopwatch timer;

        public Part3() {
            
        }

        /* Write your code in this method */
        public void Run()
        {
            timer = Stopwatch.StartNew();
            GC.PrintTime(0, timer);
            LargeObject lo;
            for (int i = 1; i <= 10000; i++) {
                lo = new LargeObject(10000);
                GC.PrintTime(i, timer);
            }
        }
    }
}
