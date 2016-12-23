CREATE PROCEDURE [dbo].[SearchPhotos]

	@SearchText nvarchar(max),
	@UserId int

AS
BEGIN
  --DECLARE @SearchText nvarchar(max) = 'DSC_0012'
  --DECLARE @UserId int = 1
  SET @SearchText = NULLIF(LTRIM(RTRIM(@SearchText)),'')
  SET @SearchText = REPLACE(Lower(@SearchText), '_', '[_]')
  PRINT @SearchText
  SELECT *
  FROM Photos
  WHERE (Lower(Title) LIKE ('%'+ @SearchText + '%')
  OR Lower(Description) like '%'+ @SearchText + '%'
  OR @SearchText is NULL)
  AND UserId = @UserId
  ORDER BY Title
END