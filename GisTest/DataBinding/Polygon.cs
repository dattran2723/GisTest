using System.Collections.Generic;

namespace GisTest.DataBinding
{
    public class Polygon
    {
        public List<Point> points { get; set; }

        public Polygon(List<Point> points)
        {
            this.points = points;
        }
    }
}