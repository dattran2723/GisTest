CREATE PROCEDURE [dbo].[DeleteThongTinCauHinh]
 @id varchar(50)
AS
BEGIN
	BEGIN TRANSACTION Dele1
		BEGIN TRY
			Delete ThongTinVeDoiTuong where ThongTinDoiTuongChinhId = @id
			Delete ThongTinDoiTuongPhu where ThongTinDoiTuongChinhId = @id
			Delete ThongTinLatLngDoiTuong where Id = @id
			Delete ThongTinDoiTuongChinh where id=@id
		COMMIT TRANSACTION Dele1
	END TRY
	BEGIN CATCH
	  ROLLBACK TRANSACTION Dele1
	END CATCH  
	select COUNT(*) from ThongTinDoiTuongChinh where id=@id
END