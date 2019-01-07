create PROCEDURE [dbo].[PagingCountry]
	@start int,
	@pageCount int,
	@softBy nvarchar(max),
	@softDir nvarchar(max),
	@search nvarchar(max),
	@total int out,
	@parentId int
AS
	DECLARE @query nvarchar(MAX)

	SET @query = N'SELECT * 
				FROM Countries 
				WHERE Name LIKE @search AND ParentId = @parentId
				ORDER BY ' + @softBy + ' ' + @softDir +  
				' OFFSET @start ROWS FETCH NEXT @pageCount ROWS ONLY;'
	DECLARE @a nvarchar(MAX);
	SET @a= '%'+@search+'%'
	EXEC sp_executesql @query,  N'@search nvarchar(MAX),@start int,@pageCount int, @parentId int', @a, @start, @pageCount, @parentId
	SET @total = (SELECT COUNT(*) FROM Countries WHERE Name LIKE '%'+@search+'%' AND ParentId = @parentId)
RETURN 0