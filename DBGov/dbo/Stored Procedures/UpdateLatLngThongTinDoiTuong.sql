CREATE PROCEDURE [dbo].[UpdateLatLngThongTinDoiTuong]
	@value varchar(max)
AS
BEGIN
	DECLARE @temp table (id varchar(50),lat float,lng float,RowNum int not null identity(1,1))
	DECLARE @id varchar(50)
	DECLARE @lat float
	DECLARE @lng float
	DECLARE @count int,@i int
	DECLARE @check varchar(20)
	DECLARE @code nvarchar(20)
	Insert into @temp
	SELECT JSON_Value(c.value, '$.id') as id, JSON_Value(c.value, '$.lat') as lat,JSON_Value(c.value, '$.lng') as lng FROM OPENJSON(@value) as c
	SET @count = (select COUNT(*) from @temp)
	Set @i=0
	While @Count <> @i
	BEGIN
		SET @i=@i+1
		Select @id = id, @lat=lat, @lng=lng from @temp where RowNum=@i
		UPDATE [dbo].[ThongTinDoiTuongChinh]
		   SET [Lat] = @lat,[Lng] = @lng
		   WHERE [Id] = @id
		select * into #temp1 from ThongTinDoiTuongPhu where ThongTinDoiTuongChinhId = @id and (KieuDuLieu = 'city' or KieuDuLieu = 'district' or KieuDuLieu = 'ward')
		set @check = (select COUNT(*) from #temp1)
		If @check > 0
		Begin
			Set @code = (select [Value] from #temp1)
			UPDATE [dbo].[Countries] SET [Lat] = @lat,[Lng] = @lng WHERE Code = @code
		End
	END
	select count(*) from ThongTinDoiTuongChinh where Id=@id
END


--exec UpdateLatLngThongTinDoiTuong '[{"id":"d868d54a-661b-6f5f-fd0f-1397bc592f81","lat":16.04333879392302,"lng":108.22500944137572}]'