CREATE TABLE [dbo].[NonprofitPrograms]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY, 
    [NonprofitId] INT NOT NULL, 
    [ProgramName] NVARCHAR(128) NOT NULL, 
    [MinAge] INT NULL, 
    [MaxAge] INT NULL, 
    [Gender] NVARCHAR(50) NULL,

	
	CONSTRAINT [FK_NonprofitPrograms_Nonprofit] FOREIGN KEY ([NonprofitId]) REFERENCES [Nonprofits](Id) ON DELETE CASCADE, 
)
