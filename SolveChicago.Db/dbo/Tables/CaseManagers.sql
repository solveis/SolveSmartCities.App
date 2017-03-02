CREATE TABLE [dbo].[CaseManagers]
(
	[Id] INT IDENTITY(1, 1) NOT NULL,
    [Email] NVARCHAR(128) NOT NULL, 
	[FirstName] NVARCHAR(128) NULL, 
	[LastName] NVARCHAR(128) NULL, 
    [ProfilePicturePath] NVARCHAR(MAX) NULL, 
	[Phone] NVARCHAR(128) NULL, 
    [CreatedDate] DATETIME2 NULL, 

    CONSTRAINT [PK_CaseManagers] PRIMARY KEY CLUSTERED ([Id] ASC), 
)
