CREATE TABLE [dbo].[NonprofitPrograms]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY, 
    [NonprofitId] INT NOT NULL, 
    [ProgramName] NVARCHAR(128) NOT NULL, 
    [MinAge] INT NULL, 
    [MaxAge] INT NULL, 
    [Gender] NVARCHAR(50) NULL,
	[EthnicityId] INT NULL, 

    CONSTRAINT [FK_NonprofitPrograms_Nonprofit] FOREIGN KEY ([NonprofitId]) REFERENCES [Nonprofits](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_NonprofitPrograms_Ethnicity] FOREIGN KEY (EthnicityId) REFERENCES [Ethnicities](Id) ON DELETE SET NULL, 
)
GO
EXEC sp_addextendedproperty N'MS_Description', N'Stores information about a Nonprofit''s Programs', 'SCHEMA', N'dbo', 'TABLE', N'NonprofitPrograms', NULL, NULL