CREATE PROCEDURE [dbo].[PagingTag]
 @start int,
 @pageCount int,
 @orderBy nvarchar(max),
 @asc bit,
 @search nvarchar(max),
 @total int out
AS
BEGIN
 SELECT Id,Ten, Code,DaSuDung ,[Order], SuDungThoiGian,ThoiGian
 FROM Tag  as t
 WHERE Ten LIKE '%'+@search+'%' OR Code LIKE '%'+@search+'%' OR TenKhongDau LIKE '%'+@search+'%'
 ORDER BY  
  CASE WHEN @orderBy='Ten' AND @asc = 1 THEN t.Ten END,
  CASE WHEN @orderBy='Ten' AND @asc = 0 THEN t.Ten END  DESC,
  CASE WHEN @orderBy='DaSuDung' AND @asc = 1 THEN t.DaSuDung END,
  CASE WHEN @orderBy='DaSuDung' AND @asc = 0 THEN t.DaSuDung END DESC,
  CASE WHEN @orderBy='Code' AND @asc = 1 THEN t.Code END,
  CASE WHEN @orderBy='Code' AND @asc = 0 THEN t.Code END DESC
 OFFSET @start ROWS FETCH NEXT @pageCount ROWS ONLY;
 SET @total = (SELECT COUNT(*) FROM Tag WHERE Ten LIKE '%'+@search+'%' OR Code LIKE '%'+@search+'%')
END
RETURN