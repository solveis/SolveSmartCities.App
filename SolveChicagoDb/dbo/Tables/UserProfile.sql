CREATE TABLE [dbo].[UserProfile]
(
	[Id] INT IDENTITY(1, 1) NOT NULL, 
    [UserName] NVARCHAR(128) NULL, 
    [Name] NVARCHAR(128) NULL, 
    [CreatedDate] DATETIME2 NULL, 
    [LastActivityDate] DATETIME2 NULL, 
    [ReceiveEmail] BIT NULL,

	CONSTRAINT [PK_UserProfile] PRIMARY KEY CLUSTERED ([Id] ASC), 
)
