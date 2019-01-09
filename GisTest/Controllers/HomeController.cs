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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">truyền vào giá trị DiaGioiHanhChinhCode</param>
        /// <returns>trả về danh sách đối tượng con của DiaGioiHanhChinhCode đó</returns>
        public JsonResult GetDiaDiem(string value)
        {
            return Json(doiTuongChinh.GetDoiTuongChinhByDiaGioiHanhChinhCode(value), JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Lấy đầy đủ một thông tin đối tượng bởi value truyền vào
        /// Nếu kích vào phường hay quận thì sẻ lấy đến thông tin của đối tượng cha nó
        /// </summary>
        /// <param name="value">truyền vào giá trị DiaGioiHanhChinhCode</param>
        /// <returns>trả về danh sách thông tin đối tượng cha và con</returns>
        public JsonResult GetFullThongTinDoiTuongByValue(string value)
        {
            List<ObjectViewModel> listObj = new List<ObjectViewModel>();
            listObj.Add(doiTuongChinh.GetThongTinDoiTuongByValue(value));
            return Json(doiTuongChinh.GetThongTinDoiTuongCha(listObj), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Khởi tạo biến result
        /// Gọi hàm IsPointInPolygon() để kiểm trả có phải điểm đó nằm trong Polygon hay không
        /// Nếu có thì sẻ gán giá trị item.Value vào result
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
        /// Lấy các giá trị trong  "coordinates" của chuổi polygon đó và thêm từng điểm Point(X,Y) vào list Point
        /// </summary>
        /// <param name="duLieuDoiTuong">truyền chuổi DuLieuDoiTuong </param>
        /// <returns>danh sách các điểm Point trong polygon đó</returns>
        public List<Point> GetPolygonFromDuLieuDoiTuong(string duLieuDoiTuong)
        {
            JObject json = JObject.Parse(duLieuDoiTuong);
            var geometry = json.SelectToken("geometry");
            string ktra = (string)geometry.SelectToken("type");
            if(ktra == "MultiPolygon")
            {
            }
            JArray coordinates = (JArray)geometry.SelectToken("coordinates")[0];
            List<Point> points = new List<Point>();
            var c = 0;
            foreach (var item in coordinates)
            {
                c++;
                //var x = item[0];
                //var y = item[1];
                //Point point = new Point((double)x, (double)y);
                //points.Add(point);
            }
            return points;
        }
        public ActionResult Page2()
        {
            return View();
        }
    }

}