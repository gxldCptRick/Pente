using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenteGame.Lib.Models
{
    public struct Point: IEquatable<Point>
    {
       public int x;
       public int y;

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (obj is Point other) return Equals(other);
            else return false;
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
