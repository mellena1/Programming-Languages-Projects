using System;
namespace comp3350_a7
{
    public class Point
    {
        private int x;
        private int y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X
        {
            get { return x; }
            set
            {
                if (value >= 0)
                    x = value;
            }
        }

        public int Y
        {
            get { return y; }
            set
            {
                if (value >= 0)
                    y = value;
            }
        }

        public override string ToString()
        {
            return "(" + x + ", " + y + ")";
        }

        public override int GetHashCode()
        {
            return (x*y).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Point other = obj as Point;
            return (other != null) && (other.x == this.x) && (other.y == this.y);
        }

        public bool Valid()
        {
            if (x < 0 || y < 0)
                return false;
            return true;
        }
    }
}
