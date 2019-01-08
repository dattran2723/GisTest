using GisTest.Models;
using GisTest.ViewModels;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GisTest.Controllers
{
    [OutputCache(Duration = 600)]
    public class HomeController : Controller
    {
        private GisData db = new GisData();
        private ThongTinDoiTuongChinh doiTuongChinh = new ThongTinDoiTuongChinh();

        public ActionResult Index()
        {
            return View(doiTuongChinh.GetDoiTuongChinhByDiaGioiHanhChinhCode("001"));
        }
        

        public JsonResult GetDiaDiem(string value)
        {
            return Json(doiTuongChinh.GetDoiTuongChinhByDiaGioiHanhChinhCode(value), JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Lấy đầy đủ một thông tin đối tượng bởi value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public JsonResult GetFullThongTinDoiTuongByValue(string value)
        {
            List<ObjectViewModel> listObj = new List<ObjectViewModel>();
            listObj.Add(doiTuongChinh.GetThongTinDoiTuongByValue(value));
            return Json(doiTuongChinh.GetThongTinDoiTuongCha(listObj), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Khởi tạo biến result
        /// Gọi hàm IsPointInPolygon() để kiểm trả có phải điểm đó nằm trong Polygon hay không
        /// Nếu có thì sẻ gán giá trị Value vào resultValue
        /// </summary>
        /// <param name="Lat">Truyền vào giá trị Lat</param>
        /// <param name="Lng">Truyền vào giá trị Lng</param>
        /// <returns>Trả về giá tri Item.value</returns>
        public JsonResult GetThongTinByLatLng(double lat, double lng)
        {
            ThongTinLatLngDoiTuong thongTinLatLngDoiTuong = new ThongTinLatLngDoiTuong();
            List<ThongTinByLatLngViewModel> list = thongTinLatLngDoiTuong.GetAllDoiTuongByLatLng(lat, lng);
            string result = string.Empty;
            foreach (var item in list)
            {
                List<Point> polygon = GetPolygonFromDuLieuDoiTuong(item.DuLieuDoiTuong);
                Point point = new Point(lng, lat);
                if (point.IsPointInPolygon(polygon))
                {
                    result = item.Value;
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// select tung phan tu trong 'coordinates' va add vao List<Point>
        /// </summary>
        /// <param name="duLieuDoiTuong">la chuoi DuLieuDoiTuong </param>
        /// <returns>list cac point cua polygon</returns>
        public List<Point> GetPolygonFromDuLieuDoiTuong(string duLieuDoiTuong)
        {
            JObject json = JObject.Parse(duLieuDoiTuong);
            var geometry = json.SelectToken("geometry");
            JArray coordinates = (JArray)geometry.SelectToken("coordinates")[0];
            List<Point> points = new List<Point>();
            foreach (var item in coordinates)
            {
                var x = item[0];
                var y = item[1];
                Point point = new Point((double)x, (double)y);
                points.Add(point);
            }
            return points;
        }
        public ActionResult Page2()
        {
            return View();
        }
    }

}