CREATE TABLE [dbo].[Schools]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY, 
    [SchoolName] NVARCHAR(255) NOT NULL, 
    [Type] NVARCHAR(255) NOT NULL
)
GO
EXEC sp_addextendedproperty N'MS_Description', N'Lookup table for Schools', 'SCHEMA', N'dbo', 'TABLE', N'Schools', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'i.e. HighSchool', 'SCHEMA', N'dbo', 'TABLE', N'Schools', 'COLUMN', N'Type'