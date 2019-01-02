namespace GisTest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThongTinVeDoiTuong")]
    public partial class ThongTinVeDoiTuong
    {
        [StringLength(50)]
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ThongTinDoiTuongChinhId { get; set; }

        public string DuLieuDoiTuong { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Order { get; set; }

        public virtual ThongTinDoiTuongChinh ThongTinDoiTuongChinh { get; set; }
    }
}
