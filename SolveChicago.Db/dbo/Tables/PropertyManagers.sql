CREATE TABLE [dbo].[PropertyManagers]
(
	[Id] INT IDENTITY(1, 1) NOT NULL,
    [Email] NVARCHAR(128) NOT NULL,
	[Name] NVARCHAR(128) NULL,
	[Phone] NVARCHAR(128) NULL, 
    [CreatedDate] DATETIME2 NULL, 

    CONSTRAINT [PK_PropertyManagers] PRIMARY KEY CLUSTERED ([Id] ASC), 
)
