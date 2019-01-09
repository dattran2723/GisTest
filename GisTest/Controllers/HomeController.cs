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
            return View(doiTuongChinh.GetDoiTuongChinhByDiaGioiHanhChinhCode("001"));
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
            return Json(doiTuongChinh.GetDoiTuongChinhByDiaGioiHanhChinhCode(value), JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Lấy đầy đủ thông tin của một đối tượng bởi code của đối tượng đó
        /// </summary>
        /// <param name="value">giá trị code của đối tượng</param>
        /// <returns>
        /// danh sách các đối tượng dưới dạng json
        /// </returns>
        public JsonResult GetFullThongTinDoiTuongByValue(string value)
        {
            List<ObjectViewModel> listObj = new List<ObjectViewModel>();
            listObj.Add(doiTuongChinh.GetThongTinDoiTuongByValue(value));
            return Json(doiTuongChinh.GetThongTinDoiTuongCha(listObj), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// truyen vao 2 gia tri Lat, Lng
        /// dem so sanh voi cac gia tri Max Min bang cach goi store
        /// trong store Lat sẽ nằm giữa voi MinLat-MaxLat va Lng sẽ nằm giữa voi MinLng-MaxLng va tra 
        /// ve gia tri ID,DuLieuDoiTuong,Value
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