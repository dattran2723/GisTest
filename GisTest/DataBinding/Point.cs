using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GisTest.DataBinding
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

        public bool IsPointInPolygon(Polygon polygon)
        {
            var points = polygon.points;
            bool inside = false;
            for (int i = 0, j = points.Count - 1; i < points.Count; j = i++)
            {
                if ((points[i].Y > Y) != (points[j].Y > Y) &&
                    (points[i].X > X || points[j].X > X) &&
                     X < (points[j].X - points[i].X) * (Y - points[i].Y) / (points[j].Y - points[i].Y) + points[i].X)
                {
                    inside = !inside;
                }
            }
            return inside;
        }
    }

}