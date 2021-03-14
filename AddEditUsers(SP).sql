CREATE PROCEDURE AddEditUsers @UserId int, @UserName varchar(20), @Email varchar(30)
AS
BEGIN
IF (@UserId =0)
INSERT INTO tblUsers (UserName, Email) VALUES (@UserName ,@Email);
ELSE
update tblUsers set UserName=@UserName, Email=@Email where UserId=@UserId ;
            END