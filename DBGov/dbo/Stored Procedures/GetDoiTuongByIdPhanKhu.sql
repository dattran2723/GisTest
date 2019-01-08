CREATE PROCEDURE [dbo].[GetDoiTuongByIdPhanKhu]
	@PhanKhuId varchar(max)
AS
BEGIN
	select ThongTinVeDoiTuong.Id as Id,ThongTinVeDoiTuong.DuLieuDoiTuong as ObjectShape,ThongTinDoiTuongChinh.PhanKhuId as LayerShape
	from ThongTinVeDoiTuong,ThongTinDoiTuongChinh 
	where ThongTinDoiTuongChinh.PhanKhuId in (select * from splitstringID(@PhanKhuId)) and ThongTinVeDoiTuong.ThongTinDoiTuongChinhId = ThongTinDoiTuongChinh.Id
END