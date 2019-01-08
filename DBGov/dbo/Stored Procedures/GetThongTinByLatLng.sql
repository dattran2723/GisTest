CREATE PROCEDURE [dbo].[GetThongTinByLatLng] @Lat nvarchar (50), @Lng nvarchar (50) 
AS SELECT
	ThongTinLatLngDoiTuong.Id,
	ThongTinVeDoiTuong.DuLieuDoiTuong,
	ThongTinDoiTuongPhu.[Value]
FROM
	ThongTinLatLngDoiTuong,
	ThongTinDoiTuongChinh,
	ThongTinDoiTuongPhu,
	ThongTinVeDoiTuong
WHERE
	(
		@Lat BETWEEN SUBSTRING (
			MinLat,
			CHARINDEX(',', MinLat) + 1,
			LEN(MinLat)
		)
		AND SUBSTRING (
			MaxLat,
			CHARINDEX(',', MaxLat) + 1,
			LEN(MaxLat)
		)
	)
AND (
	@Lng BETWEEN SUBSTRING (
		MinLng,
		0,
		CHARINDEX(',', MinLng)
	)
	AND SUBSTRING (
		MaxLng,
		0,
		CHARINDEX(',', MaxLng)
	)
)
AND ThongTinLatLngDoiTuong.Id = ThongTinDoiTuongChinh.Id
AND ThongTinDoiTuongPhu.ThongTinDoiTuongChinhId = ThongTinDoiTuongChinh.Id
AND ThongTinDoiTuongPhu.Code = 'XA/PHUONG'
AND ThongTinDoiTuongChinh.Id = ThongTinVeDoiTuong.ThongTinDoiTuongChinhId