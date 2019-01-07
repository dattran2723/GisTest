CREATE PROCEDURE [dbo].[truyvan] @Lat nvarchar(50), @Lng nvarchar(50)
AS
SELECT 
	ThongTinLatLngDoiTuong.Id,
	--ThongTinDoiTuongChinh.Ten,
	--ThongTinDoiTuongPhu.Code, 
	--ThongTinDoiTuongChinh.DiaGioiHanhChinhCode,
	ThongTinVeDoiTuong.DuLieuDoiTuong,
	ThongTinDoiTuongPhu.[Value]
FROM
	ThongTinLatLngDoiTuong,	ThongTinDoiTuongChinh, ThongTinDoiTuongPhu, ThongTinVeDoiTuong
WHERE 
( @Lat BETWEEN   SUBSTRING(MinLat,CHARINDEX(',', MinLat) +1, LEN(MinLat))
and SUBSTRING(MaxLat,CHARINDEX(',', MaxLat) +1, LEN(MaxLat))) 
AND (@Lng BETWEEN  SUBSTRING(MinLng, 0, CHARINDEX(',', MinLng))
and SUBSTRING(MaxLng, 0, CHARINDEX(',', MaxLng)) )  
AND ThongTinLatLngDoiTuong.Id = ThongTinDoiTuongChinh.Id
AND ThongTinDoiTuongPhu.ThongTinDoiTuongChinhId = ThongTinDoiTuongChinh.Id
AND ThongTinDoiTuongPhu.Code = 'XA/PHUONG'
AND ThongTinDoiTuongChinh.Id = ThongTinVeDoiTuong.ThongTinDoiTuongChinhId