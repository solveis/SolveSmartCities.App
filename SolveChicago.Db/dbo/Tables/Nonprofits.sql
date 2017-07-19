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
GO
EXEC sp_addextendedproperty N'MS_Description', N'Stores information about Nonprofits', 'SCHEMA', N'dbo', 'TABLE', N'Nonprofits', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'If the Workforce Nonprofit has more than 1 program', 'SCHEMA', N'dbo', 'TABLE', N'Nonprofits', 'COLUMN', N'HasPrograms'
GO
EXEC sp_addextendedproperty N'MS_Description', N'i.e. Workforce, Housing', 'SCHEMA', N'dbo', 'TABLE', N'Nonprofits', 'COLUMN', N'ProviderType'