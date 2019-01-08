Create PROCEDURE [dbo].[PagingUser]
	@start int,
	@pageCount int,
	@orderBy nvarchar(max),
	@asc bit,
	@search nvarchar(max),
	@total int out
AS
BEGIN
	SELECT Id,UserName,Email 
	FROM AspNetUsers 
	WHERE UserName LIKE '%'+@search+'%' OR Email LIKE '%'+@search+'%'
	ORDER BY  
		CASE WHEN @orderBy='UserName' AND @asc = 1 THEN UserName END,
		CASE WHEN @orderBy='Email' AND @asc = 1 THEN Email END,
		CASE WHEN @orderBy='UserName' AND @asc = 0 THEN UserName END DESC,
		CASE WHEN @orderBy='Email' AND @asc = 0 THEN Email END DESC
	OFFSET @start ROWS FETCH NEXT @pageCount ROWS ONLY;
	SET @total = (SELECT COUNT(*) FROM AspNetUsers WHERE UserName LIKE '%'+@search+'%' OR Email LIKE '%'+@search+'%')
END
RETURN 0