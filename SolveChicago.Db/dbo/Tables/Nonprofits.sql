CREATE TABLE [dbo].[Nonprofits]
(
	[Id] INT IDENTITY(1, 1) NOT NULL, 
    [Email] NVARCHAR(128) NULL, 
    [Name] NVARCHAR(128) NULL, 
	[Phone] NVARCHAR(128) NULL, 
    [Address1] NVARCHAR(128) NULL, 
    [Address2] NVARCHAR(50) NULL, 
    [City] NVARCHAR(50) NULL, 
    [Province] NVARCHAR(50) NULL, 
    [Country] NVARCHAR(50) NULL, 
	[ZipCode] NVARCHAR(10) NULL,
    [CreatedDate] DATETIME2 NULL, 

	[ProfilePicturePath] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_Nonprofits] PRIMARY KEY CLUSTERED ([Id] ASC), 
)
