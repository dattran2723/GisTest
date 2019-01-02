namespace GisTest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThongTinDoiTuongPhu")]
    public partial class ThongTinDoiTuongPhu
    {
        [StringLength(50)]
        public string Id { get; set; }

        [StringLength(100)]
        public string Code { get; set; }

        public string Value { get; set; }

        [Required]
        [StringLength(50)]
        public string ThongTinDoiTuongChinhId { get; set; }

        [StringLength(200)]
        public string KieuDuLieu { get; set; }

        public string Ten { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Order { get; set; }

        public virtual ThongTinDoiTuongChinh ThongTinDoiTuongChinh { get; set; }
    }
}
