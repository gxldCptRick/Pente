using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenteGame.Lib.Models
{
    public static class PointExtensions
    {
        internal static Point AddToY(this Point point, int amount)
        {
            return new Point()
            {
                x = point.x,
                y = point.y + amount
            };
        }
        internal static Point AddToX(this Point point, int amount)
        {
            return new Point()
            {
                x = point.x + amount,
                y = point.y
            };
        }
    }
}
