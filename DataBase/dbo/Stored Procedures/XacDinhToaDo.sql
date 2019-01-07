CREATE PROCEDURE [dbo].[XacDinhToaDo]
    @lat NVARCHAR(MAX) ,
    @lng NVARCHAR(MAX)
AS
    BEGIN
        SELECT  ThongTinDoiTuongChinh.Ten ,
                ThongTinDoiTuongChinh.Id ,
                ThongTinDoiTuongChinh.DiaGioiHanhChinhCode ,
                SUBSTRING(MinLat, CHARINDEX(',', MinLat) + 1, LEN(MinLat)) AS MinLat ,
                SUBSTRING(MinLat, 0, CHARINDEX(',', MinLat)) AS LngOfMinLat ,
                SUBSTRING(MaxLat, CHARINDEX(',', MaxLat) + 1, LEN(MaxLat)) AS MaxLat ,
                SUBSTRING(MaxLat, 0, CHARINDEX(',', MaxLat)) AS LngOfMaxLat ,
                SUBSTRING(MaxLng, CHARINDEX(',', MaxLng) + 1, LEN(MaxLng)) AS LatOfMaxLng ,
                SUBSTRING(MaxLng, 0, CHARINDEX(',', MaxLng)) AS MaxLng ,
                SUBSTRING(MinLng, CHARINDEX(',', MinLng) + 1, LEN(MinLng)) AS LatOfMinLng ,
                SUBSTRING(MinLng, 0, CHARINDEX(',', MinLng)) AS MinLng ,
                ThongTinVeDoiTuong.DuLieuDoiTuong
        FROM    ( ( dbo.ThongTinDoiTuongChinh
                    INNER JOIN ThongTinLatLngDoiTuong ON ThongTinDoiTuongChinh.Id = ThongTinLatLngDoiTuong.Id
                  )
                  INNER JOIN ThongTinDoiTuongPhu ON ThongTinDoiTuongChinh.Id = ThongTinDoiTuongPhu.ThongTinDoiTuongChinhId
                )
                INNER JOIN ThongTinVeDoiTuong ON ThongTinVeDoiTuong.ThongTinDoiTuongChinhId = ThongTinDoiTuongChinh.Id
        WHERE   @lat BETWEEN SUBSTRING(MinLat, CHARINDEX(',', MinLat) + 1,
                                       LEN(MinLat))
                     AND     SUBSTRING(MaxLat, CHARINDEX(',', MaxLat) + 1,
                                       LEN(MaxLat))
                AND @lng BETWEEN SUBSTRING(MinLng, 0, CHARINDEX(',', MinLng))
                         AND     SUBSTRING(MaxLng, 0, CHARINDEX(',', MaxLng))
                AND ThongTinDoiTuongPhu.Code LIKE 'XA/PHUONG'
    END