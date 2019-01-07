CREATE PROCEDURE [dbo].[SearchDoiTuong]
	@lat float,
	@lng float,
	@raduis float,
	@layer nvarchar(max)
AS
BEGIN
	DECLARE @GEO GEOGRAPHY
	SET @GEO= Geography::Point(@Lat, @lng, 4326)
	select DISTINCT  Id,LopId
	INTO #TempCount from ThongTinDoiTuongChinh
	where ThongTinDoiTuongChinh.LopId in (select * from splitstringID(@layer)) and @GEO.STDistance(Geography::Point(ISNULL(Lat,0), ISNULL(Lng,0), 4326)) <= @raduis

	select #TempCount.Id as Id, ThongTinVeDoiTuong.DuLieuDoiTuong as ObjectShape,#TempCount.LopId  as LayerShape from ThongTinVeDoiTuong,#TempCount 
		   where ThongTinVeDoiTuong.ThongTinDoiTuongChinhId=#TempCount.Id
END