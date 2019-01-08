CREATE PROCEDURE [dbo].[SearchZoomViewDoiTuong]
	@lat float,
	@lng float,
	@raduis float,
	@zoom float
AS
BEGIN
 DECLARE
  @COUNT INT --DECLARE @GEO GEOGRAPHY
  --SET @GEO= Geography::Point(@Lat, @lng, 4326)
  SELECT DISTINCT
   ThongTinDoiTuongChinh.Id,
   ThongTinDoiTuongChinh.Ten,
   ThongTinDoiTuongChinh.DiaGioiHanhChinhCode,
   Lop.ZoomMin INTO #temp
  FROM
   Lop,
   ThongTinDoiTuongChinh
  WHERE
   Lop.ZoomMin <= @zoom
  AND Lop.ZoomMax >= @zoom
  AND Lop.Id = ThongTinDoiTuongChinh.LopId
  AND Lop.CapDiaGioiHanhChinh <> 1
  AND Lop.[Status] = 1 --and @GEO.STDistance(Geography::Point(ISNULL(Lat,0), ISNULL(Lng,0), 4326)) <= @raduis
  SET @COUNT = (SELECT COUNT(*) FROM #temp)
  IF @COUNT > 0
  BEGIN
   SELECT
    ThongTinVeDoiTuong.ThongTinDoiTuongChinhId AS Id,
    ThongTinVeDoiTuong.DuLieuDoiTuong AS ObjectShape,
    ThongTinVeDoiTuong.ThongTinDoiTuongChinhId AS LayerShape
   FROM
    ThongTinVeDoiTuong,
    #temp
   WHERE
    ThongTinVeDoiTuong.ThongTinDoiTuongChinhId = #temp.Id
   ORDER BY
    #temp.ZoomMin ASC
   END
   ELSE

   BEGIN
    SELECT
     ThongTinVeDoiTuong.ThongTinDoiTuongChinhId AS Id,
     ThongTinVeDoiTuong.DuLieuDoiTuong AS ObjectShape,
     ThongTinVeDoiTuong.ThongTinDoiTuongChinhId AS LayerShape
    FROM
     Lop,
     ThongTinDoiTuongChinh,
     ThongTinDoiTuongPhu,
     ThongTinVeDoiTuong
    WHERE
     Lop.ZoomMin <= @zoom
    AND Lop.ZoomMax >= @zoom
    AND Lop.Id = ThongTinDoiTuongChinh.LopId
    AND Lop.CapDiaGioiHanhChinh = 1
    AND ThongTinDoiTuongPhu.ThongTinDoiTuongChinhId = ThongTinDoiTuongChinh.Id
    AND ThongTinDoiTuongChinh.Id = ThongTinVeDoiTuong.ThongTinDoiTuongChinhId
    AND ThongTinDoiTuongPhu.[Value] = '001032'
    ORDER BY Lop.ZoomMin ASC
    END
    END