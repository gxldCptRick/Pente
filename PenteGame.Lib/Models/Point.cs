using System;

namespace PenteGame.Lib.Models
{
    public struct Point : IEquatable<Point> //Class to represent a single point on the board
    {
        //x and y position
        public int x;
        public int y;

        //ctor
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }


        //Lovely ToString
        public override string ToString()
        {
            return $"({x},{y})";
        }

        //Override some operators
        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (obj is Point other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public static bool operator ==(Point one, Point two)
        {
            return one.Equals(two);
        }

        public static bool operator !=(Point one, Point two)
        {
            return !one.Equals(two);
        }

        public bool Equals(Point other)
        {
            return other.x == x && other.y == y;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode();
        }
    }
}
