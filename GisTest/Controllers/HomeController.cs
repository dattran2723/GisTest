using GisTest.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GisTest.Controllers
{
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
        public List<ListObjectViewModel> GetDoiTuongChinhByDiaGioiHanhChinhCode(string value)
        {
            IQueryable<ListObjectViewModel> list = from a in db.ThongTinDoiTuongChinhs
                                                   join b in db.ThongTinDoiTuongPhus
                                                   on a.Id equals b.ThongTinDoiTuongChinhId
                                                   join c in db.ThongTinVeDoiTuongs
                                                   on a.Id equals c.ThongTinDoiTuongChinhId
                                                   where a.DiaGioiHanhChinhCode == value
                                                   orderby a.Ten ascending
                                                   select new ListObjectViewModel()
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
            List<ListObjectViewModel> listObj = new List<ListObjectViewModel>();
            listObj.Add(GetThongTinDoiTuongByValue(value));
            return Json(GetThongTinDoiTuongCha(listObj), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lấy tất cả thông tin đối tượng cha
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ListObjectViewModel> GetThongTinDoiTuongCha(List<ListObjectViewModel> model)
        {
            ListObjectViewModel pa = model[model.Count - 1];
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
        public ListObjectViewModel GetThongTinDoiTuongByValue(string value)
        {
            IQueryable<ListObjectViewModel> info = from a in db.ThongTinDoiTuongChinhs
                                                   join b in db.ThongTinDoiTuongPhus on a.Id equals b.ThongTinDoiTuongChinhId
                                                   join c in db.ThongTinVeDoiTuongs on a.Id equals c.ThongTinDoiTuongChinhId
                                                   where b.Value == value
                                                   select new ListObjectViewModel
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
        /// dang sua
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult SubString(string id)
        {
            var query = from a in db.ThongTinVeDoiTuongs
                        where a.ThongTinDoiTuongChinhId == id
                        select a.DuLieuDoiTuong;
            var b = query.FirstOrDefault();
            var c = b.IndexOf("]]]}");
            var dem = b.IndexOf(":[[[");
            string chuoi = b.Substring(b.IndexOf(":[[[") + 4, c - dem);
            string[] items = chuoi.Split(',');
            List<string> list = new List<string>();
            foreach (var item in items)
            {
                list.Add(item);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
            //b.IndexOf("[[[") + 3
        }
        /// <summary>
        /// truyen vao 2 gia tri Lat, Lng
        /// dem so sanh voi cac gia tri Max Min bang cach goi store
        /// trong store se bettwen Lat voi MinLat-MaxLat va Lng voi MinLng-MaxLng
        /// </summary>
        /// <param name="Lat">value Lat</param>
        /// <param name="Lng">value Lng</param>
        /// <returns>'res' ket qua cac doi tuong thoa man dieu kien cua store </returns>
        public JsonResult GetThongTinByLatLng(string Lat, string Lng)
        {
            try
            {
                SqlParameter[] listParams = new SqlParameter[]
                {
                    new SqlParameter("@Lat", Lat),
                    new SqlParameter("@Lng", Lng),
                };
                var res = db.Database.SqlQuery<GetThongTinByLatLngViewModel>("exec truyvan @Lat, @Lng", listParams).ToList();
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}