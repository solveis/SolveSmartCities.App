CREATE TABLE [dbo].[MilitaryBranches]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY, 
    [Country] NVARCHAR(50) NULL, 
    [BranchName] NVARCHAR(50) NULL
)
GO
EXEC sp_addextendedproperty N'MS_Description', N'Lookup table for Military Branches', 'SCHEMA', N'dbo', 'TABLE', N'MilitaryBranches', NULL, NULL
