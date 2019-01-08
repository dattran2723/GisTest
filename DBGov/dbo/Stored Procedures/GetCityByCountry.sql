CREATE PROCEDURE [dbo].[GetCityByCountry]
AS
BEGIN
  Select Name from dbo.Countries
where [Level]=1 
ORDER BY Name
END