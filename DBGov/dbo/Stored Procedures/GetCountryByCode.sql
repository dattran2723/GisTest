CREATE PROCEDURE [dbo].[GetCountryByCode]
	@ParentCode nvarchar(20),
	@Code nvarchar(20)
AS
BEGIN
	DEClARE @id int
	if LEN(@ParentCode) > 0
	Begin
		Set @id = (select Id from Countries where Code = @ParentCode)
		select * from Countries where ParentId = @id order by [Name]
	End
	Else
	Begin
		Set @id = (select ParentId from Countries where Code = @Code)
		select * from Countries where ParentId = @id order by [Name]
	End
END