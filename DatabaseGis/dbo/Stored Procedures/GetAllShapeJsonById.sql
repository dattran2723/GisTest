CREATE PROCEDURE [dbo].[GetAllShapeJsonById]
	@id varchar(50)
AS
	select ThongTinVeDoiTuong.ThongTinDoiTuongChinhId as Id, ThongTinVeDoiTuong.DuLieuDoiTuong as ObjectShape from
	ThongTinVeDoiTuong
	where ThongTinVeDoiTuong.ThongTinDoiTuongChinhId = @id
RETURN 0