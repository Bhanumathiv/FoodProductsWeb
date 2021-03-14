CREATE TABLE [dbo].[tblProducts] (
    [PId]          INT          IDENTITY (101, 1) NOT NULL,
    [ProductName]  VARCHAR (20) NULL,
    [Price]        DECIMAL (18) NULL,
    [availability] INT          NULL
);

