CREATE FUNCTION [dbo].[GetLat]
(	
	@stringToSplit VARCHAR(max),
	@check bit
)
RETURNS float  
AS
Begin
	DECLARE @lat float
	DECLARE @lng float
	DECLARE @pos INT
	SELECT @lng =0
	SELECT @lat=0
	SELECT @pos  = CHARINDEX(',', @stringToSplit)
	SELECT @lng = convert(float,SUBSTRING(@stringToSplit, 1, @pos-1))
	SELECT @stringToSplit = SUBSTRING(@stringToSplit, @pos+1, LEN(@stringToSplit)-@pos)
	SELECT @pos  = CHARINDEX(',', @stringToSplit)
	if @pos <= 0
		SELECT @lat = convert(float,@stringToSplit)
	ELse
	    SELECT @lat = convert(float,SUBSTRING(@stringToSplit, 1, @pos-1))
	if @check =1
	BEGIN
		RETURN @lng;
	END
	RETURN @lat;
	
End