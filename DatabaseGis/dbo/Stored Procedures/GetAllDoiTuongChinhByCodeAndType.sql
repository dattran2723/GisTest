CREATE PROCEDURE [dbo].[GetAllDoiTuongChinhByCodeAndType]
	@code varchar(100),
	@type varchar(200)
AS
	select 
		ThongTinDoiTuongChinh.Id, 
		ThongTinDoiTuongChinh.CauHinhDoiTuongId, 
		ThongTinDoiTuongChinh.DiaGioiHanhChinhCode,
		ThongTinDoiTuongChinh.Lat,
		ThongTinDoiTuongChinh.Lng,
		ThongTinDoiTuongChinh.LopId,
		ThongTinDoiTuongChinh.SoThua,
		ThongTinDoiTuongChinh.SoTo,
		ThongTinDoiTuongChinh.Ten
	from
	ThongTinDoiTuongChinh inner join ThongTinDoiTuongPhu on ThongTinDoiTuongChinh.Id = ThongTinDoiTuongPhu.ThongTinDoiTuongChinhId
	where ThongTinDoiTuongPhu.Value = @code and ThongTinDoiTuongPhu.KieuDuLieu like '%'+@type+'%'
RETURN 0