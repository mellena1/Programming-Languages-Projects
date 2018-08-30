using System;
namespace comp3350_lab8
{

    /* This class builds an object that allocates some
     * heap space. The default constructor allocates
     * 1000 doubles, and a constructor with a parameter
     * allows for some flexibility in object size.
     */
    public class LargeObject
    {
        public int size;
        public double[] arr;

        public LargeObject()
        {
            size = 1000;
            arr = new double[size];
        }

        public LargeObject(int s)
        {
            size = s;
            arr = new double[size];
        }

    }
}
