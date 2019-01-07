CREATE PROCEDURE [dbo].[GetFrameShapesByKieuDuLieuAndValue]
	@KieuDuLieu varchar(200),
	@Value nvarchar(MAX)
AS
	SELECT ThongTinLatLngDoiTuong.MaxLat, ThongTinLatLngDoiTuong.MaxLng, ThongTinLatLngDoiTuong.MinLat,  ThongTinLatLngDoiTuong.MinLng, ThongTinDoiTuongChinh.Lat, ThongTinDoiTuongChinh.Lng from
	ThongTinLatLngDoiTuong INNER JOIN ThongTinDoiTuongPhu On ThongTinLatLngDoiTuong.Id = ThongTinDoiTuongPhu.ThongTinDoiTuongChinhId INNER JOIN ThongTinDoiTuongChinh ON ThongTinDoiTuongPhu.ThongTinDoiTuongChinhId = ThongTinDoiTuongChinh.Id
	Where (ThongTinDoiTuongPhu.KieuDuLieu = @KieuDuLieu or ThongTinDoiTuongPhu.KieuDuLieu = 'add-'+@KieuDuLieu)  AND ThongTinDoiTuongPhu.Value = @Value
RETURN 0