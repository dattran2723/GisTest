CREATE PROCEDURE [dbo].[SearchViewDoiTuong]
	@lat float,
	@lng float,
	@raduis float,
	@zoom float
AS
BEGIN
	DECLARE @GEO GEOGRAPHY
	SET @GEO= Geography::Point(@Lat, @lng, 4326)
	SELECT DISTINCT ThongTinDoiTuongChinh.Id,ThongTinDoiTuongChinh.Ten,ThongTinDoiTuongChinh.DiaGioiHanhChinhCode,Lop.ZoomMin INTO #temp
	  FROM Lop,ThongTinDoiTuongChinh
	  WHERE Lop.Id = ThongTinDoiTuongChinh.LopId AND Lop.[Status] = 1 AND Lop.ZoomMin <= @zoom AND Lop.ZoomMax >= @zoom 
			AND @GEO.STDistance(Geography::Point(ISNULL(Lat,0), ISNULL(Lng,0), 4326)) <= @raduis
	SELECT
		ThongTinVeDoiTuong.ThongTinDoiTuongChinhId AS Id,ThongTinVeDoiTuong.DuLieuDoiTuong AS ObjectShape,ThongTinVeDoiTuong.ThongTinDoiTuongChinhId AS LayerShape
	   FROM ThongTinVeDoiTuong, #temp
	   WHERE ThongTinVeDoiTuong.ThongTinDoiTuongChinhId = #temp.Id ORDER BY #temp.ZoomMin ASC
END