CREATE PROCEDURE [dbo].[GetAllShapJsonByParentPlaceId]
	@parentPlaceId int
AS
	SELECT Code, Type into #CodeTemp from Countries where ParentId = @parentPlaceId 
	SELECT ThongTinVeDoiTuong.ThongTinDoiTuongChinhId as Id, ThongTinVeDoiTuong.DuLieuDoiTuong as ObjectShape from
	ThongTinVeDoiTuong INNER JOIN ThongTinDoiTuongPhu On ThongTinVeDoiTuong.ThongTinDoiTuongChinhId = ThongTinDoiTuongPhu.ThongTinDoiTuongChinhId INNER JOIN #CodeTemp ON ThongTinDoiTuongPhu.Value = #CodeTemp.Code AND ThongTinDoiTuongPhu.KieuDuLieu = #CodeTemp.Type
RETURN 0