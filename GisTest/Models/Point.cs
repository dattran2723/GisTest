using System.Collections.Generic;

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
        /// điều kiện Id: vd đường thẳng AB và điểm P(Xp,Yp) (là điểm cần so sánh có nằm trong polygon không)
        ///     + Kiểm tra 2 tọa độ Ya, Yb của điểm A, B có nằm khác phía với Yp hay không
        ///     + Kiểm tra 2 tọa độ Xa, Yb của điểm A, B, có nằm khác phía với Xp hay không
        ///     + Giả sử Kẻ đường thẳng từ P song song với Ox cắt AB tại điểm M(Xm,Ym)
        ///     Nếu Xp < Xm thì điểm P sẻ nằm trong polygon đó
        /// </summary>
        /// <param name="polygon">truyền tham số vào bằng 1 dánh sách các điểm</param>
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