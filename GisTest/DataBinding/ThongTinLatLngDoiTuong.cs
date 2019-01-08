namespace GisTest.Models
{
    using GisTest.ViewModels;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.SqlClient;
    using System.Linq;

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

        private GisData db = new GisData();
        /// <summary>
        /// trong Store sẻ Lấy Lat so sah với MinLat-MaxLat và lấy Lng so sánh với MinLng-MaxLng
        /// Và lấy tới ThongTinDoiTuongPhu.Code = 'XA/PHUONG'
        /// </summary>
        /// <param name="lat">truyền vào giá trị Lat</param>
        /// <param name="lng">truyền vào giá trị Lat</param>
        /// <returns>1 list bao gồm Id, DuLieuDoiTuong và Value</returns>
        public List<ThongTinByLatLngViewModel> GetAllDoiTuongByLatLng(double lat, double lng)
        {
            try
            {
                SqlParameter[] listParams = new SqlParameter[]
                {
                    new SqlParameter("@Lat", lat),
                    new SqlParameter("@Lng", lng),
                };
                var result = db.Database.SqlQuery<ThongTinByLatLngViewModel>("exec GetThongTinByLatLng @Lat, @Lng", listParams).ToList();
                return result;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
