namespace GisTest.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class Country
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public int? Level { get; set; }

        [StringLength(100)]
        public string Type { get; set; }

        public int? ParentId { get; set; }

        public int? ModuleId { get; set; }

        public int? IsVisible { get; set; }

        public DateTime? CreatedOnDate { get; set; }

        public int? CreatedByUserId { get; set; }

        public DateTime? LastModifiedOnDate { get; set; }

        public int? LastModifiedByUserId { get; set; }

        public bool? IsState { get; set; }

        [StringLength(20)]
        public string Code { get; set; }

        public double? Lat { get; set; }

        public double? Lng { get; set; }

        [StringLength(100)]
        public string NameKhongDau { get; set; }
        
    }
}
