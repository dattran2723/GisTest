using GisTest.Models;
using System;
using System.Collections.Generic;
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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}