CREATE PROCEDURE [dbo].[InsertDoiTuong]
	@Id varchar(50),
	@Ten nvarchar(max),
	@SoThua int,
	@SoTo int,
	@LopId varchar(50),
	@DiaGioHanhChinh nvarchar(20),
	@CauHinhDoiTuong varchar(50),
	@lat float,
	@lng float
AS
BEGIN
	INSERT INTO ThongTinDoiTuongChinh(Id, Ten, SoThua, SoTo, LopId, DiaGioiHanhChinhCode, Lat, Lng, CauHinhDoiTuongId) 
	VALUES(@Id, @Ten,@SoThua, @SoTo, @LopId,@DiaGioHanhChinh, @lat, @lng, @CauHinhDoiTuong)
	select count(*) from ThongTinDoiTuongChinh where Id= @Id
END
RETURN 0