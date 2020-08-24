﻿CREATE TABLE [dbo].[Rental]
(
	[CreationDate] DATETIME NOT NULL, 
    [Good_Id] INT NOT NULL, 
    [User_Id] INT NOT NULL, 
    [RentedFrom] DATETIME NOT NULL, 
    [RentedTo] DATETIME NOT NULL, 
    [Amount] FLOAT NOT NULL,
    [Deposit] MONEY NULL, 
    [Rating] INT NULL, 
    [Review] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_Rental] PRIMARY KEY ([CreationDate], [Good_Id], [User_Id]), 
    CONSTRAINT [FK_Rental_ToUsers] FOREIGN KEY ([User_Id]) REFERENCES [Users]([User_Id]), 
    CONSTRAINT [FK_Rental_ToGood] FOREIGN KEY ([Good_Id]) REFERENCES [Good]([Good_Id]) 
)
