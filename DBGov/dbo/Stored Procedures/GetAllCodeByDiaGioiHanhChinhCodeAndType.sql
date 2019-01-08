CREATE PROCEDURE [dbo].[GetAllCodeByDiaGioiHanhChinhCodeAndType]
	@diaGioiHanhChinhCode nvarchar(20),
	@type varchar(200)
AS
	select distinct ThongTinDoiTuongPhu.Value 
	from ThongTinDoiTuongPhu inner join ThongTinDoiTuongChinh on ThongTinDoiTuongPhu.ThongTinDoiTuongChinhId = ThongTinDoiTuongChinh.Id
	where ThongTinDoiTuongChinh.DiaGioiHanhChinhCode = @diaGioiHanhChinhCode and ThongTinDoiTuongPhu.KieuDuLieu like '%'+@type+'%' 
RETURN 0