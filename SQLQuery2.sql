CREATE PROCEDURE UserByID
@UserId int
AS 
BEGIN
SET NOCOUNT ON;
Select * from tblUsers where UserId=@UserId
END