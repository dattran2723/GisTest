using GisTest.Models;
using GisTest.ViewModels;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace GisTest.DataBinding
{
    public class ThongTinLatLngDoiTuong
    {
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