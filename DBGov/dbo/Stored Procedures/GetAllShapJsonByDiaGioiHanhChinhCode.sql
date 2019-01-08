CREATE PROCEDURE [dbo].[GetAllShapJsonByDiaGioiHanhChinhCode]
	@diaGioiHanhChinhCode nvarchar(20)
AS
	select ThongTinVeDoiTuong.ThongTinDoiTuongChinhId as Id, ThongTinVeDoiTuong.DuLieuDoiTuong as ObjectShape from
	ThongTinVeDoiTuong inner join ThongTinDoiTuongChinh on ThongTinVeDoiTuong.ThongTinDoiTuongChinhId = ThongTinDoiTuongChinh.Id
	where ThongTinDoiTuongChinh.DiaGioiHanhChinhCode = @diaGioiHanhChinhCode
RETURN 0