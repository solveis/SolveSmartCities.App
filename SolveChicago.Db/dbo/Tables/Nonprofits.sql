CREATE TABLE [dbo].[Nonprofits]
(
	[Id] INT IDENTITY(1, 1) NOT NULL, 
    [Email] NVARCHAR(128) NULL, 
    [Name] NVARCHAR(128) NULL, 
    [CreatedDate] DATETIME2 NULL, 
	[ProfilePicturePath] NVARCHAR(MAX) NULL, 

    CONSTRAINT [PK_Nonprofits] PRIMARY KEY CLUSTERED ([Id] ASC), 
)
