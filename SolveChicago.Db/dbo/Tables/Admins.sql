CREATE TABLE [dbo].[Admins]
(
	[Id] INT IDENTITY(1, 1) NOT NULL,
    [Email] NVARCHAR(128) NOT NULL, 
	[FirstName] NVARCHAR(128) NULL, 
	[LastName] NVARCHAR(128) NULL, 
    [ProfilePicturePath] NCHAR(10) NULL, 
	[Phone] NVARCHAR(128) NULL, 
    [CreatedDate] DATETIME2 NULL, 
	[InvitedBy] INT NULL, 

    CONSTRAINT [PK_Admins] PRIMARY KEY CLUSTERED ([Id] ASC), 
)
