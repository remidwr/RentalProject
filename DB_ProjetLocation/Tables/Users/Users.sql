﻿CREATE TABLE [dbo].[Users]
(
	[User_Id] INT NOT NULL IDENTITY, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [BirthDate] DATE NOT NULL, 
    [Email] NVARCHAR(320) NOT NULL, 
    [Passwd] BINARY(64) NOT NULL,
    [Street] NVARCHAR(120) NULL, 
    [Number] NVARCHAR(10) NULL, 
    [Box] NVARCHAR(10) NULL, 
    [PostCode] INT NULL, 
    [City] NVARCHAR(120) NULL, 
    [Phone1] NVARCHAR(50) NULL, 
    [Phone2] NVARCHAR(50) NULL, 
    [Picture] NVARCHAR(320) NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsBanned] BIT NOT NULL DEFAULT 0, 
    [Role_Id] INT NOT NULL DEFAULT 1,
    CONSTRAINT [PK_Users] PRIMARY KEY ([User_Id]), 
    CONSTRAINT [UK_Users_Email] UNIQUE ([Email]), 
    CONSTRAINT [FK_Users_ToRoles] FOREIGN KEY ([Role_Id]) REFERENCES [Roles]([Role_Id]), 
    CONSTRAINT [CK_Users_BirthDate] CHECK (BirthDate > '1900-01-01' AND BirthDate < GetDate())
)

GO

CREATE TRIGGER [dbo].[Trigger_Users_Delete]
    ON [dbo].[Users]
    INSTEAD OF DELETE
    AS
    BEGIN
        DECLARE @UserId INT;

        SET @UserId = (SELECT [User_Id] FROM deleted);

        UPDATE Users
        SET [IsActive] = 0
        WHERE [User_Id] = @UserId AND IsActive = 1;
    END
GO


CREATE UNIQUE INDEX [IX_Users_Email] ON [dbo].[Users] ([Email])
