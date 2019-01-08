CREATE PROCEDURE [dbo].[PhanTrangPhanKhu]
	@start int,
	@pageCount int,
	@softBy nvarchar(max),
	@softDir nvarchar(max),
	@search nvarchar(max),
	@total int out
AS
	DECLARE @query nvarchar(MAX), @searchTemp varchar(MAX)
	set @searchTemp = dbo.fChuyenCoDauThanhKhongDau('%' +@search+'%')
	SET @query = N'SELECT PhanKhu.*,Countries.Description as MoTaDonVi, Countries.Name as TenDonVi 
				FROM PhanKhu inner join Countries on PhanKhu.DiaGioiHanhChinhCode = Countries.Code 
				WHERE PhanKhu.TenTimKiem LIKE @search ORDER BY ' + @softBy + ' ' + @softDir +  ' OFFSET @start ROWS FETCH NEXT @pageCount ROWS ONLY;'
	EXEC sp_executesql @query,  N'@search nvarchar(max),@start int,@pageCount int',@searchTemp, @start, @pageCount
	SET @total = (SELECT COUNT(*) FROM PhanKhu WHERE TenTimKiem LIKE  @searchTemp)
RETURN 0