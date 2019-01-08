CREATE FUNCTION [dbo].[parseJSONInforShap]
(	
	@ListThongTin nvarchar(max)
)
RETURNS @return Table (Ten NVARCHAR(max),Code VARCHAR(100),KieuDuLieu VARCHAR(200),DuLieuMacDinh NVARCHAR(max),RowNum int not null identity(1,1))
AS
BEGIN
    Insert into @return
	Select *  
	from OPENJSON(@ListThongTin)
	WITH(Ten NVARCHAR(max) N'$.Ten', 
	  Code VARCHAR(100) '$.Code', 
	  KieuDuLieu VARCHAR(200) '$.KieuDuLieu', 
	  DuLieuMacDinh NVARCHAR(max) N'$.DuLieuMacDinh')
   Return
End