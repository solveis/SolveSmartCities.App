CREATE TABLE [dbo].[Skills]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(MAX) NOT NULL, 
    [Level] INT NULL, 
    [IsWorkforce] BIT NOT NULL DEFAULT 1
)
GO
EXEC sp_addextendedproperty N'MS_Description', N'Lookup table for Skills', 'SCHEMA', N'dbo', 'TABLE', N'Skills', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'1 = Workforce, 0 = SoftSkill', 'SCHEMA', N'dbo', 'TABLE', N'Skills', 'COLUMN', N'IsWorkforce'
GO
EXEC sp_addextendedproperty N'MS_Description', N'i.e. High, Medium, Low', 'SCHEMA', N'dbo', 'TABLE', N'Skills', 'COLUMN', N'Level'