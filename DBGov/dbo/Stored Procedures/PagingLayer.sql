CREATE PROCEDURE [dbo].[PagingLayer]
	@start int,
	@pageCount int,
	@softBy nvarchar(max),
	@softDir nvarchar(max),
	@search nvarchar(max),
	@total int out
AS
BEGIN
	DECLARE @query nvarchar(MAX)

	SET @query = N'SELECT * 
				FROM Lop 
				WHERE Ten LIKE @search
				ORDER BY ' + @softBy + ' ' + @softDir +  
				' OFFSET @start ROWS FETCH NEXT @pageCount ROWS ONLY;'
	DECLARE @a nvarchar(MAX);
	SET @a= '%'+@search+'%'
	EXEC sp_executesql @query,  N'@search nvarchar(MAX),@start int,@pageCount int', @a, @start, @pageCount
	SET @total = (SELECT COUNT(*) FROM Lop WHERE Ten LIKE '%'+@search+'%')
END
RETURN 0