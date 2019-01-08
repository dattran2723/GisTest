CREATE PROCEDURE [dbo].[GetPhanKhuByZoomView]
	@zoom float,
	@lat float,
	@lng float,
	@raduis float
AS
BEGIN
    DECLARE @codePhan varchar(20)
    DECLARE @GEO GEOGRAPHY
	SET @GEO= Geography::Point(@Lat, @lng, 4326)
	select Top 1 DiaGioiHanhChinhCode
	INTO #TempCount from ThongTinDoiTuongChinh
	where  @GEO.STDistance(Geography::Point(ISNULL(Lat,0), ISNULL(Lng,0), 4326)) <= @raduis and LEN(DiaGioiHanhChinhCode) > 6
	set @codePhan = SUBSTRING((select * from #TempCount), 1, 6)
	select PhanKhu.Id,PhanKhu.TenPhanKhu,PhanKhu.DiaGioiHanhChinhCode,PhanKhu.Lat,PhanKhu.Lng,PhanKhu.ParentId,PhanKhu.Zoom 
	   from PhanKhu,Countries,Lop 
	   where PhanKhu.DiaGioiHanhChinhCode = Countries.Code and Countries.[Level] = Lop.CapDiaGioiHanhChinh 
	         and Lop.ZoomMin <= @zoom and Lop.ZoomMax>=@zoom and PhanKhu.DiaGioiHanhChinhCode like ''+@codePhan+'%'

END