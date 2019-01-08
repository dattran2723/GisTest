CREATE PROCEDURE [dbo].[GetChiTietThongTinByLopId]
	@CauHinhDoiTuongId varchar(50),
	@IdShape varchar(50)
AS
BEGIN
	DECLARE @Count int
	Select ThongTinDoiTuongPhu.Ten,ThongTinDoiTuongPhu.Code,ThongTinDoiTuongPhu.KieuDuLieu,ThongTinDoiTuongPhu.[Value] as DuLieuMacDinh, ThongTinDoiTuongPhu.[Order] into #temp
	from ThongTinDoiTuongPhu,ThongTinDoiTuongChinh
	where ThongTinDoiTuongChinh.CauHinhDoiTuongId=@CauHinhDoiTuongId and ThongTinDoiTuongChinh.Id=@IdShape and ThongTinDoiTuongPhu.ThongTinDoiTuongChinhId=ThongTinDoiTuongChinh.Id
	Set @Count =(select count(*) from #temp)
	If @Count <= 0
	Begin
		Select ChiTietCauHinhDoiTuong.Ten,ChiTietCauHinhDoiTuong.Code,ChiTietCauHinhDoiTuong.KieuDuLieu,ChiTietCauHinhDoiTuong.DuLieuMacDinh from CauHinhDoiTuong,ChiTietCauHinhDoiTuong
		where CauHinhDoiTuong.Id=ChiTietCauHinhDoiTuong.CauHinhDoiTuongId and TrangThaiThuocTinh = 0  and ChiTietCauHinhDoiTuong.[Status]=1 and CauHinhDoiTuong.Id=@CauHinhDoiTuongId
		Order by [Order] asc
	End
	Else
	Begin
	  Select Ten,Code,KieuDuLieu,DuLieuMacDinh from #temp order by [Order] asc
	End
END