using System.Collections.Generic;

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
        /// <summary>
        /// gán biến inside = false
        /// điều kiện Id: vd đường thẳng AB và điểm P(Xp,Yp) (là điểm cần so sánh có nằm trong polygon không)
        ///     + Kiểm tra 2 tọa độ Ya, Yb của điểm A, B có nằm khác phía với Yp hay không
        ///     + Kiểm tra 2 tọa độ Xa, Yb của điểm A, B, có nằm khác phía với Xp hay không
        ///     + Giả sử Kẻ đường thẳng từ P song song với Ox cắt AB tại điểm M(Xm,Ym)
        ///     Nếu Xp < Xm thì điểm P sẻ nằm trong polygon đó
        /// </summary>
        /// <param name="polygon">truyền tham số vào bằng 1 dánh sách các điểm</param>
        /// <returns>inside == true thì điểm đó nằm trong Polygon còn ngược lại == fasle thì sẻ nằm ngoài</returns>
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