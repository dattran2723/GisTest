using GisTest.DataBinding;
using GisTest.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GisTest.Controllers
{
    [OutputCache(Duration = 600)]
    public class HomeController : Controller
    {
        private ThongTinDoiTuongChinh doiTuongChinh = new ThongTinDoiTuongChinh();

        public ActionResult Index()
        {
            List<ObjectViewModel> result = doiTuongChinh.GetDoiTuongChinhByDiaGioiHanhChinhCode("001");
            return View(result);
        }
        
        /// <summary>
        /// Lấy các thông tin đối tượng bởi DiaGioiHanhChinhCode
        /// </summary>
        /// <param name="value"> giá trị của DiaGioiHanhChinhCode </param>
        /// <returns>
        /// Danh sách đối tượng có DiaGioiHanhChinhCode đó dưới dạng json
        /// </returns>
        public JsonResult GetDiaDiem(string value)
        {
            List<ObjectViewModel> result = doiTuongChinh.GetDoiTuongChinhByDiaGioiHanhChinhCode(value);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Lấy đầy đủ thông tin đối tượng bởi value truyền vào
        /// Nếu kích vào phường hay quận thì sẻ lấy đến thông tin của đối tượng cha nó
        /// </summary>
        /// <param name="value">truyền vào giá trị DiaGioiHanhChinhCode</param>
        /// <returns>trả về danh sách thông tin đối tượng cha và con</returns>
        public JsonResult GetFullThongTinDoiTuongByValue(string value)
        {
            List<ObjectViewModel> listObj = new List<ObjectViewModel>();
            listObj.Add(doiTuongChinh.GetThongTinDoiTuongByValue(value));
            listObj = doiTuongChinh.GetThongTinDoiTuongCha(listObj);
            return Json(listObj, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Khởi tạo biến result
        /// Gọi hàm IsPointInPolygon() để kiểm trả có phải điểm đó nằm trong Polygon hay không
        /// Nếu có thì sẻ gán giá trị item.Value vào result
        /// </summary>
        /// <param name="Lat">giá trị Lat</param>
        /// <param name="Lng">giá trị Lng</param>
        /// <returns>giá trị Code của một đối tượng</returns>
        public JsonResult GetThongTinByLatLng(double lat, double lng)
        {
            ThongTinLatLngDoiTuong thongTinLatLngDoiTuong = new ThongTinLatLngDoiTuong();
            ThongTinVeDoiTuong thongTinVeDoiTuong = new ThongTinVeDoiTuong();

            List<ThongTinByLatLngViewModel> list = thongTinLatLngDoiTuong.GetAllDoiTuongByLatLng(lat, lng);
            string valueDoiTuong = string.Empty;

            foreach (var item in list)
            {
                List<Polygon> polygons = thongTinVeDoiTuong.GetPolygonsFromDuLieuDoiTuong(item.DuLieuDoiTuong);
                Point point = new Point(lng, lat);
                bool result = false;
                foreach (var polygon in polygons)
                {
                    result = point.IsPointInPolygon(polygon) ? true : result;
                }
                valueDoiTuong = result ? item.Value : valueDoiTuong;
            }
            return Json(valueDoiTuong, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Page2()
        {
            return View();
        }
    }

}