CREATE PROCEDURE [dbo].[GetQuanHuyenByCity]
@code varchar(50)
AS
BEGIN
  Select * from dbo.ThongTinDoiTuongChinh INNER JOIN ThongTinDoiTuongPhu 
  ON ThongTinDoiTuongChinh.Id=ThongTinDoiTuongPhu.ThongTinDoiTuongChinhId
	where DiaGioiHanhChinhCode like @code
	And ThongTinDoiTuongPhu.Code = 'HUYEN/QUAN'
END