namespace GisTest.Models
{
    using GisTest.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
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
        /// Lấy tất cả đổi tượng có Lat Lng nằm trong
        /// </summary>
        /// <param name="lat">giá trị Lat của điểm</param>
        /// <param name="lng">giá trị Lng của điển</param>
        /// <returns>
        /// danh sách các đối tượng
        /// </returns>
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
