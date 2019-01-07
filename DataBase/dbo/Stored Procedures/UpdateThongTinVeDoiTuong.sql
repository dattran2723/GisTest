CREATE PROCEDURE [dbo].[UpdateThongTinVeDoiTuong] 
	@value nvarchar(max)
AS
BEGIN
	DECLARE @temp table (Id varchar(50),ObjectShape nvarchar(max),LayerShape nvarchar(40),RowNum int)
	DECLARE @id varchar(50)
	DECLARE @objectShape nvarchar(max)
	DECLARE @layerShape nvarchar(50)
	DECLARE @count int,@i int
	Insert into @temp
	Select * FROM dbo.parseJSONShape(@value)
	SET @count = (select COUNT(*) from @temp)
	Set @i=0
	While @Count <> @i
	BEGIN
		SET @i=@i+1
		SET @id = (Select Id from @temp where RowNum=@i)
		SET @objectShape = (Select ObjectShape from @temp where RowNum=@i)
		SET @layerShape = (Select LayerShape from @temp where RowNum=@i)
		--IF((Select COUNT(*) from MapShape where IdShape = @id) <= 0)
		IF((Select COUNT(*) from ThongTinVeDoiTuong where Id = @id) <= 0)
		Begin
			INSERT INTO [dbo].[ThongTinVeDoiTuong]
			   ([Id],[DuLieuDoiTuong],ThongTinDoiTuongChinhId)
			VALUES(@id,@objectShape,@layerShape)
		END
		ELSE
		Begin
			UPDATE [dbo].[ThongTinVeDoiTuong]
			   SET [DuLieuDoiTuong] = @objectShape,
				   ThongTinDoiTuongChinhId = @layerShape
			 WHERE [Id] = @id
		END
	END
	select COUNT(*) from @temp
END



--alter PROCEDURE [dbo].[UpdateMapShape] 
--	@value nvarchar(max)
--AS
--BEGIN
--	DECLARE @temp table (Id varchar(50),ObjectShape nvarchar(max),LayerShape nvarchar(40),RowNum int)
--	DECLARE @id varchar(50)
--	DECLARE @objectShape nvarchar(max)
--	DECLARE @layerShape nvarchar(50)
--	DECLARE @count int,@i int
--	Insert into @temp
--	Select * FROM dbo.parseJSONShape(@value)
--	SET @count = (select COUNT(*) from @temp)
--	Set @i=0
--	While @Count <> @i
--	BEGIN
--		SET @i=@i+1
--		SET @id = (Select Id from @temp where RowNum=@i)
--		SET @objectShape = (Select ObjectShape from @temp where RowNum=@i)
--		SET @layerShape = (Select LayerShape from @temp where RowNum=@i)
--		IF((Select COUNT(*) from MapShape where IdShape = @id) <= 0)
--		Begin
--			INSERT INTO [dbo].[MapShape]
--			   ([IdShape],[ObjectShape],LayerShape)
--			VALUES(@id,@objectShape,@layerShape)
--		END
--		ELSE
--		Begin
--			UPDATE [dbo].[MapShape]
--			   SET [ObjectShape] = @objectShape,
--				   LayerShape = @layerShape
--			 WHERE [IdShape] = @id
--		END
--	END
--	select @Count
--END