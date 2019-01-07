CREATE PROCEDURE [dbo].[UpdateThongTinLatLngDoiTuong]
	@id varchar(50),
	@MaxLat varchar(200),
	@MinLat varchar(200),
	@MaxLng varchar(200),
	@MinLng varchar(200),
	@Width float,
	@Height float
AS
BEGIN
	DECLARE @count int
	Set @count = (select COUNT(*) from ThongTinLatLngDoiTuong where Id = @id)
	If @count > 0
	Begin
		UPDATE [dbo].[ThongTinLatLngDoiTuong]
		   SET [MaxLat] = @MaxLat
			  ,[MinLat] = @MinLat
			  ,[MaxLng] = @MaxLng
			  ,[MinLng] = @MinLng
			  ,[Width] = @Width
			  ,[Height] = @Height
		 WHERE [Id] = @id
	End
	Else
	Begin 
		INSERT INTO [dbo].[ThongTinLatLngDoiTuong]([Id],[MaxLat],[MinLat],[MaxLng],[MinLng],[Width],[Height])
		VALUES (@id,@MaxLat,@MinLat,@MaxLng,@MinLng,@Width,@Height)
	End 
	select COUNT(*) from ThongTinLatLngDoiTuong where Id = @id
END