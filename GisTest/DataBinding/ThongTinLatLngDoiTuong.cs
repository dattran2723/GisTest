using GisTest.Models;
using GisTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GisTest.DataBinding
{
    public class ThongTinLatLngDoiTuong
    {
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