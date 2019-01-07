using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GisTest.Models
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public bool IsPointInPolygon(List<Point> polygon)
        {
            bool inside = false;
            int c = 0;
            for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
            {
                if ((polygon[i].Y > Y) != (polygon[j].Y > Y) &&
                    (polygon[i].X >= X || polygon[j].X >= X) &&
                     X < (polygon[j].X - polygon[i].X) * (Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X)
                {
                    inside = !inside;
                    c++;
                }
            }
            return inside;
        }
    }

}