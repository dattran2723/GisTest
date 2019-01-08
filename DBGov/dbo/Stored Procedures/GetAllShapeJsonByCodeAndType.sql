CREATE PROCEDURE [dbo].[GetAllShapeJsonByCodeAndType]
	@code varchar(100),
	@type varchar(200)
AS
	select ThongTinVeDoiTuong.ThongTinDoiTuongChinhId as Id, ThongTinVeDoiTuong.DuLieuDoiTuong as ObjectShape from
	ThongTinVeDoiTuong inner join ThongTinDoiTuongPhu on ThongTinVeDoiTuong.ThongTinDoiTuongChinhId = ThongTinDoiTuongPhu.ThongTinDoiTuongChinhId
	where ThongTinDoiTuongPhu.Value = @code and ThongTinDoiTuongPhu.KieuDuLieu like '%'+@type+'%'
RETURN 0