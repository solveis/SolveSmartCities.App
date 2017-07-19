CREATE TABLE [dbo].[Interests]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(MAX) NOT NULL
)
GO
EXEC sp_addextendedproperty N'MS_Description', N'Lookup table for Interests', 'SCHEMA', N'dbo', 'TABLE', N'Interests', NULL, NULL
