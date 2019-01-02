namespace GisTest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
    }
}
