CREATE PROCEDURE DeleteUserByID
@UserId int
AS 
BEGIN
SET NOCOUNT ON;
Delete from tblUsers where UserId=@UserId
END