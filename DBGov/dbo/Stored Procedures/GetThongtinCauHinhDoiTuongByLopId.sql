CREATE PROCEDURE [dbo].[GetThongtinCauHinhDoiTuongByLopId]
	@id varchar(50),
	@idthongtindoituongchinh varchar(50)
AS
BEGIN
    If (LEN(@idthongtindoituongchinh) <= 0)
	Begin
		DECLARE @IdCauHinhDoiTuong varchar(50)
		set @IdCauHinhDoiTuong=(select top 1 CauHinhDoiTuong.Id from CauHinhDoiTuong,LopCauHinhDoiTuong where LopCauHinhDoiTuong.LopId=@id and LopCauHinhDoiTuong.CauHinhDoiTuongId =CauHinhDoiTuong.Id)
		select ChiTietCauHinhDoiTuong.Ten,ChiTietCauHinhDoiTuong.Code,ChiTietCauHinhDoiTuong.KieuDuLieu,ChiTietCauHinhDoiTuong.DuLieuMacDinh , ChiTietCauHinhDoiTuong.[Order] INTO #temp
		from ChiTietCauHinhDoiTuong,CauHinhDoiTuong
		where ChiTietCauHinhDoiTuong.CauHinhDoiTuongId = CauHinhDoiTuong.Id and TrangThaiThuocTinh =0 and CauHinhDoiTuong.Id = @IdCauHinhDoiTuong and ChiTietCauHinhDoiTuong.[Status] = 1
		order by ChiTietCauHinhDoiTuong.[Order] asc
		insert into #temp
		select 'Ten' as [Ten],'CauHinhDoiTuongId' as [Code],'Key' as KieuDuLieu, @IdCauHinhDoiTuong as DuLieuMacDinh, 0 as [Order]
		select Ten,Code,KieuDuLieu,DuLieuMacDinh from #temp order by [Order] asc
	End
	Else
	Begin
		select ThongTinDoiTuongPhu.Ten,ThongTinDoiTuongPhu.Code,ThongTinDoiTuongPhu.KieuDuLieu,ThongTinDoiTuongPhu.[Value] as DuLieuMacDinh
		from ThongTinDoiTuongChinh,ThongTinDoiTuongPhu
		where ThongTinDoiTuongChinh.Id= ThongTinDoiTuongPhu.ThongTinDoiTuongChinhId and ThongTinDoiTuongPhu.ThongTinDoiTuongChinhId = @idthongtindoituongchinh
		order by [Order] asc
	End
END


--exec GetThongtinCauHinhDoiTuongByLopId 'a2135b43-ac6c-4527-bfa8-58447fac602e',''