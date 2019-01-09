using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GisTest.Models
{
    [Table("ThongTinLatLngDoiTuong")]
    public partial class ThongTinLatLngDoiTuong
    {
        [StringLength(50)]
        public string Id { get; set; }

        [StringLength(200)]
        public string MaxLat { get; set; }

        [StringLength(200)]
        public string MinLat { get; set; }

        [StringLength(200)]
        public string MaxLng { get; set; }

        [StringLength(200)]
        public string MinLng { get; set; }

        public double? Width { get; set; }

        public double? Height { get; set; }

    }
}
