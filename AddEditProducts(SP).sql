CREATE PROCEDURE AddEditProducts @PId int, @ProductName varchar(20), 	@Price decimal, @availability int
AS
BEGIN
IF (@PId=0)
INSERT INTO tblProducts (ProductName, Price, availability) VALUES (@ProductName,@Price,@availability);
ELSE
update tblProducts set ProductName=@ProductName, Price=@Price, availability=@availability where PId=@PId;
            END