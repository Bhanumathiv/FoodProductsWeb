CREATE PROCEDURE ProductviewbyID
@PId int
AS 
BEGIN
SET NOCOUNT ON;
Select * from tblProducts where PId=@PId
END