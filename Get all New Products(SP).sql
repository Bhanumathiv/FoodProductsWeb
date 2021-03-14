CREATE PROCEDURE ViewAllNewProduct
AS 
BEGIN
SET NOCOUNT ON;
Select * from tblProducts where availability=0
END