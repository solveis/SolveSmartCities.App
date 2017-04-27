CREATE TABLE [dbo].[MilitaryBranches]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY, 
    [Country] NVARCHAR(50) NULL, 
    [BranchName] NVARCHAR(50) NULL
)
