CREATE TABLE [dbo].[Admins]
(
	[Id] INT IDENTITY(1, 1) NOT NULL,
    [Email] NVARCHAR(128) NOT NULL, 
	[Name] NVARCHAR(128) NULL, 
    [ProfilePicturePath] NCHAR(10) NULL, 
	[Phone] NVARCHAR(128) NULL, 
    [Address1] NVARCHAR(128) NULL, 
    [Address2] NVARCHAR(50) NULL, 
    [City] NVARCHAR(50) NULL, 
    [Province] NVARCHAR(50) NULL, 
    [Country] NVARCHAR(50) NULL, 
    [CreatedDate] DATETIME2 NULL, 
	[InvitedBy] NVARCHAR(128) NULL, 

    CONSTRAINT [PK_Admins] PRIMARY KEY CLUSTERED ([Id] ASC), 
)
