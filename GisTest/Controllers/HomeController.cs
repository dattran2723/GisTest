using GisTest.Models;
using GisTest.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
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
            return Json(GetThongTinDoiTuongCha(listObj), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lấy tất cả thông tin đối tượng cha
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ObjectViewModel> GetThongTinDoiTuongCha(List<ObjectViewModel> model)
        {
            ObjectViewModel pa = model[model.Count - 1];
            if (pa != null)
            {
                model.Add(doiTuongChinh.GetThongTinDoiTuongByValue(pa.DiaGioiHanhChinhCode));
                return GetThongTinDoiTuongCha(model);
            }
            model.Remove(pa);
            return model;
        }

        
        /// <summary>
        /// truyen vao 2 gia tri Lat, Lng
        /// dem so sanh voi cac gia tri Max Min bang cach goi store
        /// trong store se bettwen Lat voi MinLat-MaxLat va Lng voi MinLng-MaxLng va tra 
        /// ve gia tri ID,DuLieuDoiTuong,Value
        /// </summary>
        /// <param name="Lat">value Lat</param>
        /// <param name="Lng">value Lng</param>
        /// <returns>phan tu Value dau tien </returns>
        public JsonResult GetThongTinByLatLng(double lat, double lng)
        {
            ThongTinLatLngDoiTuong obj = new ThongTinLatLngDoiTuong();
            List<ThongTinByLatLngViewModel> listObj = obj.GetAllDoiTuongByLatLng(lat, lng);
            string result = string.Empty;
            foreach (var item in listObj)
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
        /// <param name="dulieudoituong">la chuoi DuLieuDoiTuong </param>
        /// <returns>list cac point cua polygon</returns>
        public List<Point> GetPolygonFromDuLieuDoiTuong(string dulieudoituong)
        {
            JObject json = JObject.Parse(dulieudoituong);
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