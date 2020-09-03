﻿CREATE TABLE [dbo].[Rental]
(
	[Rental_Id] INT NOT NULL IDENTITY, 
    [Good_Id] INT NOT NULL, 
    [Owner_Id] INT NOT NULL, 
    [Tenant_Id] INT NOT NULL,
    [CreationDate] DATETIME NOT NULL,
    [RentedFrom] DATETIME NOT NULL, 
    [RentedTo] DATETIME NOT NULL, 
    [Amount] FLOAT NOT NULL,
    [Deposit] FLOAT NULL,
    [Rating] INT NULL, 
    [Review] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_Rental] PRIMARY KEY ([Rental_Id], [Good_Id], [Owner_Id], [Tenant_Id]), 
    CONSTRAINT [FK_Rental_ToOwner] FOREIGN KEY ([Owner_Id]) REFERENCES [Users]([User_Id]), 
    CONSTRAINT [FK_Rental_ToTenant] FOREIGN KEY ([Tenant_Id]) REFERENCES [Users]([User_Id]), 
    CONSTRAINT [FK_Rental_ToGood] FOREIGN KEY ([Good_Id]) REFERENCES [Good]([Good_Id]) 
)