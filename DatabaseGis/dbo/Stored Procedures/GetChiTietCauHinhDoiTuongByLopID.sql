CREATE PROCEDURE [dbo].[GetChiTietCauHinhDoiTuongByLopID]
	@id varchar(50)
AS
BEGIN
	DECLARE @IdCauHinhDoiTuong varchar(50)
	set @IdCauHinhDoiTuong=(select top 1 CauHinhDoiTuong.Id from CauHinhDoiTuong,LopCauHinhDoiTuong where LopCauHinhDoiTuong.LopId=@id and LopCauHinhDoiTuong.CauHinhDoiTuongId =CauHinhDoiTuong.Id)
	select ChiTietCauHinhDoiTuong.Ten as [Text], ChiTietCauHinhDoiTuong.DuLieuMacDinh as [Value] INTO #temp
	from ChiTietCauHinhDoiTuong,CauHinhDoiTuong
	where ChiTietCauHinhDoiTuong.CauHinhDoiTuongId = CauHinhDoiTuong.Id and TrangThaiThuocTinh =1 and CauHinhDoiTuong.Id = @IdCauHinhDoiTuong
	insert into #temp
	select 'templatecategory' as [Text], CauHinhDoiTuong.Id as [Value]
	from CauHinhDoiTuong where CauHinhDoiTuong.Id = @IdCauHinhDoiTuong
	select * from #temp
END