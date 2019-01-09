using GisTest.ViewModels;
using System.Collections.Generic;

namespace GisTest.DataBinding
{
    public interface IThongTinLatLngDoiTuong
    {
        List<ThongTinByLatLngViewModel> GetAllDoiTuongByLatLng(double lat, double lng);
    }
}
