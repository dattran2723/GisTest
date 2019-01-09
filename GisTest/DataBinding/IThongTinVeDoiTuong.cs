using System.Collections.Generic;

namespace GisTest.DataBinding
{
    public interface IThongTinVeDoiTuong
    {
        List<Polygon> GetPolygonsFromDuLieuDoiTuong(string duLieuDoiTuong);
    }
}
