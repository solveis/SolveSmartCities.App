CREATE TABLE [dbo].[Skills]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(MAX) NOT NULL, 
    [Level] INT NULL, 
    [IsWorkforce] BIT NOT NULL DEFAULT 1
)
