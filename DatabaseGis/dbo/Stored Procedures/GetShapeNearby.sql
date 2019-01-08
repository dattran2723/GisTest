CREATE PROCEDURE GetShapeNearby
	@Id varchar(50),
	@raduis float,
	@lat float,
	@lng float
AS
BEGIN
	DECLARE @GEO GEOGRAPHY
	SET @GEO= Geography::Point(@Lat, @lng, 4326)
	DECLARE @layerId varchar(50)
	Set @layerId = (select LopId from ThongTinDoiTuongChinh where Id = @Id)

	select DISTINCT  ThongTinDoiTuongChinh.Id,ThongTinDoiTuongChinh.LopId
	INTO #TempCount from ThongTinDoiTuongChinh, ThongTinLatLngDoiTuong
	where ThongTinDoiTuongChinh.LopId = @layerId and ThongTinDoiTuongChinh.Id = ThongTinLatLngDoiTuong.Id and ThongTinDoiTuongChinh.Id <> @Id 
	and ThongTinLatLngDoiTuong.MaxLat IS NOT NULL and ThongTinLatLngDoiTuong.MinLat IS NOT NULL and ThongTinLatLngDoiTuong.MaxLng IS NOT NULL and ThongTinLatLngDoiTuong.MinLng IS NOT NULL
	and (
	      @GEO.STDistance(Geography::Point(ISNULL((select dbo.GetLat(ThongTinLatLngDoiTuong.MaxLat,0)),0), ISNULL((select dbo.GetLat(ThongTinLatLngDoiTuong.MaxLat,1)),0), 4326)) <= @raduis
		  or @GEO.STDistance(Geography::Point(ISNULL((select dbo.GetLat(ThongTinLatLngDoiTuong.MinLat,0)),0), ISNULL((select dbo.GetLat(ThongTinLatLngDoiTuong.MinLat,1)),0), 4326)) <= @raduis
		  or @GEO.STDistance(Geography::Point(ISNULL((select dbo.GetLat(ThongTinLatLngDoiTuong.MaxLng,0)),0), ISNULL((select dbo.GetLat(ThongTinLatLngDoiTuong.MaxLng,1)),0), 4326)) <= @raduis
		  or @GEO.STDistance(Geography::Point(ISNULL((select dbo.GetLat(ThongTinLatLngDoiTuong.MinLng,0)),0), ISNULL((select dbo.GetLat(ThongTinLatLngDoiTuong.MinLng,1)),0), 4326)) <= @raduis
		)

	select #TempCount.Id as Id, ThongTinVeDoiTuong.DuLieuDoiTuong as ObjectShape,#TempCount.LopId  as LayerShape from ThongTinVeDoiTuong,#TempCount 
		   where ThongTinVeDoiTuong.ThongTinDoiTuongChinhId=#TempCount.Id
	--select * from #TempCount
END