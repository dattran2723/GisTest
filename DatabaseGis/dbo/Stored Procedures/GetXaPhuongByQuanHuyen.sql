CREATE PROCEDURE [dbo].[GetXaPhuongByQuanHuyen]
@quanId VARCHAR(50)
AS
BEGIN
  Select ThongTinDoiTuongChinh.Ten from dbo.ThongTinDoiTuongChinh INNER JOIN ThongTinDoiTuongPhu
	on ThongTinDoiTuongChinh.Id= ThongTinDoiTuongPhu.ThongTinDoiTuongChinhId
	where ThongTinDoiTuongPhu.ThongTinDoiTuongChinhId like @quanId
	
END