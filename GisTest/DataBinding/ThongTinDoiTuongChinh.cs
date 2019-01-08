namespace GisTest.Models
{
    using GisTest.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("ThongTinDoiTuongChinh")]
    public partial class ThongTinDoiTuongChinh
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThongTinDoiTuongChinh()
        {
            ThongTinDoiTuongPhus = new HashSet<ThongTinDoiTuongPhu>();
            ThongTinVeDoiTuongs = new HashSet<ThongTinVeDoiTuong>();
        }

        [StringLength(50)]
        public string Id { get; set; }

        public string Ten { get; set; }

        public int? SoThua { get; set; }

        public int? SoTo { get; set; }

        [Required]
        [StringLength(50)]
        public string LopId { get; set; }

        [StringLength(20)]
        public string DiaGioiHanhChinhCode { get; set; }

        public double? Lat { get; set; }

        public double? Lng { get; set; }

        [Required]
        [StringLength(50)]
        public string CauHinhDoiTuongId { get; set; }

        [StringLength(50)]
        public string PhanKhuId { get; set; }

        public string Tag { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThongTinDoiTuongPhu> ThongTinDoiTuongPhus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThongTinVeDoiTuong> ThongTinVeDoiTuongs { get; set; }

        private GisData db = new GisData();


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
    }
}
