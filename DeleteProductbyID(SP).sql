CREATE PROCEDURE DeleteProductbyID
@PId int
AS 
BEGIN
SET NOCOUNT ON;
Delete from tblProducts where PId=@PId
END