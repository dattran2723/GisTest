using GisTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GisTest.Controllers
{
    public class TestController : Controller
    {
        private Point point = new Point(3, 4);
        private List<Point> poly = new List<Point>() { new Point(1,1), new Point(2,5), new Point(7,8), new Point(9,2) };
        // GET: Test
        public void Index()
        {
            var par =Check(point, poly);
        }
        public bool Check(Point p, List<Point> polygon)
        {
            var inside = false;
            for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
            {
                var l = polygon[i].X;
                var k = polygon[j].Y;
                var a = polygon[i].Y > p.Y;
                var b = polygon[j].Y > p.Y;
                var c = polygon[j].X - polygon[i].X;
                var d = p.Y - polygon[i].Y;
                var e = polygon[j].Y - polygon[i].Y;
                var v = (polygon[j].X - polygon[i].X) * (p.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X;
                var vz = (polygon[j].X - polygon[i].X) * (p.Y - polygon[i].Y) / ((polygon[j].Y - polygon[i].Y) + polygon[i].X);

                if ((polygon[i].Y > p.Y) != (polygon[j].Y > p.Y) &&
                     p.X < (polygon[j].X - polygon[i].X) * (p.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X)
                {
                    inside = !inside;
                }
            }

            return inside;
        }
    }
}