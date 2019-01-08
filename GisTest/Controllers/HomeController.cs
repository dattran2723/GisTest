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
        public ActionResult Index()
        {
            return View(GetDoiTuongChinhByDiaGioiHanhChinhCode("001"));
        }

        /// <summary>
        /// Lấy thông tin đối tượng chỉnh bởi "DiaGioiHanhChinhCode"
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <returns>
        /// danh sách các đối tượng có "DiaGioiHanhChinhCode" = value
        /// </returns>
        public List<ObjectViewModel> GetDoiTuongChinhByDiaGioiHanhChinhCode(string value)
        {
            IQueryable<ObjectViewModel> list = from a in db.ThongTinDoiTuongChinhs
                                               join b in db.ThongTinDoiTuongPhus
                                               on a.Id equals b.ThongTinDoiTuongChinhId
                                               where a.DiaGioiHanhChinhCode == value
                                               orderby a.Ten ascending
                                               select new ObjectViewModel()
                                               {
                                                   Id = a.Id,
                                                   Ten = a.Ten,
                                                   Value = b.Value,
                                                   Lat = a.Lat,
                                                   Lng = a.Lng
                                               };
            return list.ToList();
        }

        public JsonResult GetDiaDiem(string value)
        {
            return Json(GetDoiTuongChinhByDiaGioiHanhChinhCode(value), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lấy đầy đủ một thông tin đối tượng bởi value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public JsonResult GetFullThongTinDoiTuongByValue(string value)
        {
            List<ObjectViewModel> listObj = new List<ObjectViewModel>();
            listObj.Add(GetThongTinDoiTuongByValue(value));
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
                model.Add(GetThongTinDoiTuongByValue(pa.DiaGioiHanhChinhCode));
                return GetThongTinDoiTuongCha(model);
            }
            model.Remove(pa);
            return model;
        }

        /// <summary>
        /// Lấy thông tin đối tượng bởi value
        /// </summary>
        /// <param name="value"></param>
        /// <returns>
        /// Đối tượng có value = value
        /// </returns>
        public ObjectViewModel GetThongTinDoiTuongByValue(string value)
        {
            IQueryable<ObjectViewModel> info = from a in db.ThongTinDoiTuongChinhs
                                               join b in db.ThongTinDoiTuongPhus on a.Id equals b.ThongTinDoiTuongChinhId
                                               join c in db.ThongTinVeDoiTuongs on a.Id equals c.ThongTinDoiTuongChinhId
                                               where b.Value == value
                                               select new ObjectViewModel
                                               {
                                                   Id = a.Id,
                                                   Ten = a.Ten,
                                                   Value = b.Value,
                                                   Code = b.Code,
                                                   Lat = a.Lat,
                                                   Lng = a.Lng,
                                                   DuLieuVe = c.DuLieuDoiTuong,
                                                   DiaGioiHanhChinhCode = a.DiaGioiHanhChinhCode,
                                                   Zoom = (b.Code == "XA/PHUONG" ? 12 : (b.Code == "HUYEN/QUAN" ? 11 : 9))
                                               };
            return info.FirstOrDefault();
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

            //foreach (var item in res)
            //{
            //    List<Point> listPoint = GetDuLieuDoiTuong(item.DuLieuDoiTuong);
            //    Point point = new Point(Lng, Lat);
            //    var kqtrave = point.IsPointInPolygon(listPoint);
            //    if (kqtrave == true)
            //    {
            //        result = item.Value;
            //    }
            //}
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// select tung phan tu trong 'coordinates' va add vao List<Point>
        /// </summary>
        /// <param name="dulieu">la chuoi DuLieuDoiTuong </param>
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
        public bool test()
        {
            List<Point> listPoint = new List<Point>() { new Point(1, 1), new Point(2, 4), new Point(5, 1), new Point(3, 2) };
            Point point = new Point(2, 3);
            var res = point.IsPointInPolygon(listPoint);
            return res;
        }
        public ActionResult Page2()
        {
            return View();
        }
    }

}