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
        /// <summary>
        /// gán biến inside = false
        /// điều kiện Id: vd đường thẳng AB và điểm M
        ///     + Kiểm tra 2 tọa độ Y của điểm A, B có nằm khác phía với Ym hay không
        ///     + Kiểm tra 2 tọa độ X của điểm A, B, 1 trong 2 tọa độ X đó lớn hơn Xm thì sẻ thỏa
        ///     + Từ M kẻ đường thẳng song song Ox, cắt với AM tại điểm P, nếu Xm < Xp thì M sẻ nằm trong Polygon đó
        /// </summary>
        /// <param name="polygon">truyền tham số vào bằng 1 List<Point></param>
        /// <returns>inside == true thì điểm đó nằm trong Polygon còn ngược lại == fasle thì sẻ nằm ngoài</returns>
        public bool IsPointInPolygon(List<Point> polygon)
        {
            bool inside = false;
            for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
            {
                if ((polygon[i].Y > Y) != (polygon[j].Y > Y) &&
                    (polygon[i].X > X || polygon[j].X > X) &&
                     X < (polygon[j].X - polygon[i].X) * (Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X)
                {
                    inside = !inside;
                }
            }
            return inside;
        }
    }

}