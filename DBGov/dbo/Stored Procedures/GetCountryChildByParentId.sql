CREATE PROCEDURE [dbo].[GetCountryChildByParentId]
	@Id int,
	@Code nvarchar(20)
AS
BEGIN
	
	If @Id >=0
	Begin
		Select Countries.Code as [Text], Countries.[Name] as [Name] from Countries where ParentId = @Id
	End
	Else
	Begin
		DECLARE @idParent int
	    SET @idParent = (Select Id from Countries where Code = @Code)
		Select Countries.Code as [Text], Countries.[Name] as [Name] from Countries where ParentId = @idParent
	End
END