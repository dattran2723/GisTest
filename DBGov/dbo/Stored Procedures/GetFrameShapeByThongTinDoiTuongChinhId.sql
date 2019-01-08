CREATE PROCEDURE [dbo].[GetFrameShapeByThongTinDoiTuongChinhId]
	@id varchar(50)
AS
	SELECT ThongTinLatLngDoiTuong.MaxLat, ThongTinLatLngDoiTuong.MaxLng, ThongTinLatLngDoiTuong.MinLat,  ThongTinLatLngDoiTuong.MinLng, ThongTinDoiTuongChinh.Lat, ThongTinDoiTuongChinh.Lng from
	ThongTinLatLngDoiTuong INNER JOIN ThongTinDoiTuongChinh ON ThongTinLatLngDoiTuong.Id = ThongTinDoiTuongChinh.Id
	Where ThongTinDoiTuongChinh.Id = @id
RETURN 0