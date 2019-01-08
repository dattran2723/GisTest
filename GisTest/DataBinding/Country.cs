namespace GisTest.Models
{
    using GisTest.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

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

        private GisData db = new GisData();

        //public void GetFullThongTinDoiTuong(List<ObjectViewModel> list ,string code)
        //{
        //    List<ObjectViewModel> listObj = new List<ObjectViewModel>();

        //}
        public void GetDoiTuongByID(int id)
        {
            var list = from a in db.Countries
                       where a.Id == id
                       select new ObjectViewModel
                       {
                           Id = a.Id.ToString(),
                           Ten = a.Name,
                           Level = a.Level,
                           Description = a.Description
                       };
        }
    }
}
