using GisTest.Models;
using GisTest.ViewModels;
using System.Collections.Generic;
using System.Linq;
namespace GisTest.Models
{
    using GisTest.ViewModels;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

namespace GisTest.DataBinding
{
    public partial class ThongTinDoiTuongChinh
    {
        private GisData db = new GisData();


        /// <summary>
        /// Lấy thông tin đối tượng chỉnh bởi "DiaGioiHanhChinhCode"
        /// </summary>
        /// <param name="value"> giá trị "DiaGioiHanhChinhCode" </param>
        /// <returns>
        /// danh sách các đối tượng có "DiaGioiHanhChinhCode" = value
        /// </returns>
        public List<ObjectViewModel> GetDoiTuongChinhByDiaGioiHanhChinhCode(string value)
        {
            IQueryable<ObjectViewModel> list = from a in db.ThongTinDoiTuongChinhs
                                               join b in db.ThongTinDoiTuongPhus on a.Id equals b.ThongTinDoiTuongChinhId
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

        /// <summary>
        /// Lấy thông tin đối tượng bởi value
        /// </summary>
        /// <param name="value">DiaGioiHanhChinhCode của đối tượng</param>
        /// <returns>
        /// Đối tượng có value = value truyền vào
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
        /// Lấy tất cả thông tin đối tượng cha
        /// </summary>
        /// <param name="model">Danh sách các đối tượng</param>
        /// <returns>
        /// danh sách các đối tượng
        /// </returns>
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
    }
}