using GisTest.ViewModels;
using System.Collections.Generic;

namespace GisTest.DataBinding
{
    public interface IThongTinDoiTuongChinh
    {
        List<ObjectViewModel> GetDoiTuongChinhByDiaGioiHanhChinhCode(string value);
        ObjectViewModel GetThongTinDoiTuongByValue(string value);
        List<ObjectViewModel> GetThongTinDoiTuongCha(List<ObjectViewModel> model);
    }
}