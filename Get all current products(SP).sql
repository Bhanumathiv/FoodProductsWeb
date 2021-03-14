CREATE PROCEDURE Viewallproduct
AS 
BEGIN
SET NOCOUNT ON;
Select * from tblProducts where availability=1
END