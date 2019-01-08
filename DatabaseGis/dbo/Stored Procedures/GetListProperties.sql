CREATE PROCEDURE [dbo].[GetListProperties]
	@CategoryId varchar(50)
AS
BEGIN
	Select [Ten] as [Text], [DuLieuMacDinh] as [Value]
	from ChiTietCauHinhDoiTuong
	where TrangThaiThuocTinh = 1 and CauHinhDoiTuongId=@CategoryId
	order by [Order]
END


--exec GetListProperties 'f4e61dbe-fc67-4213-b0dd-d80842120b86'