CREATE FUNCTION [dbo].[parseJSONShape]
(	
	@value nvarchar(max)
)
RETURNS 
	@returnList TABLE (Id varchar(40),ObjectShape nvarchar(max),LayerShape nvarchar(40),RowNum int not null identity(1,1))
AS
BEGIN
	 DECLARE @id VARCHAR(40)
	 DECLARE @objectShape nVARCHAR(max)
	 DECLARE @layerShape nvarchar(40)
	 DeClare @TempSetting nvarchar(max)
	 DECLARE @pos INT
	 DECLARE @pos1 INT
	 Declare @i int
	 SET @i=0
	 SET @TempSetting = @value
	 --DECLARE @SettingKey nVARCHAR(max)
	 --DECLARE @SettingKey1 nVARCHAR(max)	
	 SELECT @TempSetting = SUBSTRING(@TempSetting, 2, LEN(@TempSetting))
	 While CHARINDEX('{"Id":"', @TempSetting) > 0
		Begin
			--select @value
			SELECT @pos = CHARINDEX('{"Id":"', @TempSetting)
			SELECT @TempSetting = SUBSTRING(@TempSetting, @pos+7, LEN(@TempSetting))
			--select @value
			SELECT @pos1 = CHARINDEX('","', @TempSetting)
			SET @id = SUBSTRING(@TempSetting, 1, @pos1-1)
			--select @SettingKey
			SELECT @TempSetting = SUBSTRING(@TempSetting, @pos1, LEN(@TempSetting))
			--select @TempSetting

			SELECT @pos = CHARINDEX('","ObjectShape":"', @TempSetting)
			SELECT @TempSetting = SUBSTRING(@TempSetting, @pos+LEN('","ObjectShape":"'), LEN(@TempSetting))
			--replay
			SELECT @TempSetting=(SELECT REPLACE(@TempSetting, '\', ''));
			--select @TempSetting
			SELECT @pos1 = CHARINDEX('}}"', @TempSetting)
			--SELECT @pos1
			SET @objectShape = SUBSTRING(@TempSetting, 1, @pos1+(LEN('}}')-1))
			--select @SettingKey
			SELECT @TempSetting = SUBSTRING(@TempSetting, @pos1+(LEN('}}"')), LEN(@TempSetting))

			SELECT @pos = CHARINDEX(',"LayerShape":"', @TempSetting)
			SELECT @TempSetting = SUBSTRING(@TempSetting, @pos+LEN(',"LayerShape":"'), LEN(@TempSetting))

			SELECT @pos1 = CHARINDEX('"}', @TempSetting)
			SET @layerShape = SUBSTRING(@TempSetting, 1, @pos1-1)

			SELECT @TempSetting = SUBSTRING(@TempSetting, @pos1+(LEN('"},')), LEN(@TempSetting))

			
			INSERT INTO @returnList (Id,ObjectShape,LayerShape)
			SELECT @id,@objectShape,@layerShape
		end
	RETURN 
END