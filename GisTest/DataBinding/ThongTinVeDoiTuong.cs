using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GisTest.DataBinding
{
    public partial class ThongTinVeDoiTuong : IThongTinVeDoiTuong
    {
        /// <summary>
        /// Lấy các poylgon từ DuLieuDoiTuong
        /// </summary>
        /// <param name="dulieudoituong">la chuoi DuLieuDoiTuong </param>
        /// <returns>danh sách các polygon</returns>
        public List<Polygon> GetPolygonsFromDuLieuDoiTuong(string duLieuDoiTuong)
        {
            JObject json = JObject.Parse(duLieuDoiTuong);
            var geometry = json.SelectToken("geometry");
            var type = (string)geometry.SelectToken("type");
            JArray coordinates = (JArray)geometry.SelectToken("coordinates");
            List<Polygon> listPolygon = new List<Polygon>();

            if (type == "MultiPolygon")
            {
                foreach (var polygon in coordinates)
                {
                    List<Point> points = new List<Point>();
                    foreach (var item in polygon[0])
                    {
                        var x = item[0];
                        var y = item[1];
                        Point point = new Point((double)x, (double)y);
                        points.Add(point);
                    }
                    listPolygon.Add(new Polygon(points));
                }
            }
            else
            {
                List<Point> points = new List<Point>();
                foreach (var item in coordinates[0])
                {
                    var x = item[0];
                    var y = item[1];
                    Point point = new Point((double)x, (double)y);
                    points.Add(point);
                }
                listPolygon.Add(new Polygon(points));
            }

            return listPolygon;
        }
    }
}