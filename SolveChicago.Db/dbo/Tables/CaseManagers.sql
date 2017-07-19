CREATE TABLE [dbo].[CaseManagers]
(
	[Id] INT IDENTITY(1, 1) NOT NULL,
    [Email] NVARCHAR(128) NOT NULL, 
	[FirstName] NVARCHAR(128) NULL, 
	[LastName] NVARCHAR(128) NULL, 
	[Gender] NVARCHAR(50) NULL,
	[Birthday] DATETIME2 NULL,
    [ProfilePicturePath] NVARCHAR(MAX) NULL, 
    [CreatedDate] DATETIME2 NULL, 
    [UserId] NVARCHAR(128) NULL, 
    CONSTRAINT [PK_CaseManagers] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_CaseManagers_AspNetUser] FOREIGN KEY (UserId) REFERENCES [AspNetUsers](Id) ON DELETE SET NULL, 
)
GO
EXEC sp_addextendedproperty N'MS_Description', N'Stores information about CaseManagers', 'SCHEMA', N'dbo', 'TABLE', N'CaseManagers', NULL, NULL