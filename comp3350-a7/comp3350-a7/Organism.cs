using System;
using System.Collections.Generic;
namespace comp3350_a7
{
    public abstract class Organism
    {
        public static Dictionary<Type, int> numOfEach = new Dictionary<Type, int>();
        protected int timeUntilReproduce;
        protected int timeSinceReproduce;

        protected Random randomNum;

        public Organism(Type organismType, int timeUntilReproduce)
        {
            addToOrganismAmt(organismType, 1);
            this.randomNum = new Random();
            this.timeUntilReproduce = timeUntilReproduce;
            this.timeSinceReproduce = 0;
        }

        public static void addToOrganismAmt(Type organismType, int change)
        {
            if (!numOfEach.ContainsKey(organismType))
            {
                numOfEach.Add(organismType, 1);
            } else {
                int cur = numOfEach[organismType];
                numOfEach[organismType] = cur+change;
            }
        }

        public static string getOrganismAmts() {
            string output = "";
            foreach (KeyValuePair<Type, int> entry in numOfEach)
            {
                output += entry.Key.Name + ": " + entry.Value + " ";
            }
            return output;
        }

        public static bool somethingExtinct() {
            foreach (KeyValuePair<Type, int> entry in numOfEach)
            {
                if (entry.Value == 0) return true;
            }
            return false;
        }

        protected Point Reproduce(ISet<Point> freeSpaces)
        {
            if (timeSinceReproduce < timeUntilReproduce) {
                timeSinceReproduce++;
                return null;
            }

            if (freeSpaces.Count == 0) return null;
            int spaceToGrow = randomNum.Next(0, freeSpaces.Count);

            int count = 0;
            Point p = null;
            foreach (var point in freeSpaces)
            {
                if (count == spaceToGrow)
                {
                    p = point;
                    break;
                }
                count++;   
            }
            return p;
        }
    }
}
