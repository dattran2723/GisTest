CREATE FUNCTION [dbo].[SplitJsonToTableShape] (@value ntext)
RETURNS 
	@returnList TABLE (Id varchar(50),ObjectShape varchar(max),RowNum int not null identity(1,1))
AS
BEGIN
	INSERT INTO @returnList (Id,ObjectShape)
	(Select
       max(case when name='Id' then convert(varchar(50),StringValue) else '' end) as [Id],
       max(case when name='ObjectShape' then convert(nvarchar(max),StringValue) else '' end) as [ObjectShape]
	From parseJSON (@value)
	where ValueType = 'string' OR ValueType = 'boolean'
	group by parent_ID)
	
	RETURN 
END