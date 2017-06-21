CREATE TABLE [dbo].[NonprofitPrograms]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY, 
    [NonprofitId] INT NOT NULL, 
    [ProgramName] NVARCHAR(128) NOT NULL, 
    [MinAge] INT NULL, 
    [MaxAge] INT NULL, 
    [Gender] NVARCHAR(50) NULL,

)
