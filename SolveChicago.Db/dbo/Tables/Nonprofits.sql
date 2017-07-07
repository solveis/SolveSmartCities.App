CREATE TABLE [dbo].[Nonprofits]
(
	[Id] INT IDENTITY(1, 1) NOT NULL, 
    [Email] NVARCHAR(128) NULL, 
    [Name] NVARCHAR(128) NULL, 
    [CreatedDate] DATETIME2 NULL, 
	[ProfilePicturePath] NVARCHAR(MAX) NULL, 
    [ProviderType] NVARCHAR(50) NULL, 
    [HasPrograms] BIT NULL, 
    CONSTRAINT [PK_Nonprofits] PRIMARY KEY CLUSTERED ([Id] ASC), 
)
