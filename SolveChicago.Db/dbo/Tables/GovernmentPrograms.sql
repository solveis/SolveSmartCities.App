CREATE TABLE [dbo].[GovernmentPrograms]
(
	[Id] INT IDENTITY(1, 1) NOT NULL, 
    [Name] NVARCHAR(MAX) NOT NULL,
	[MinAge] INT NULL, 
    [MaxAge] INT NULL, 
    [MinIncome] MONEY NULL, 
    [MaxIncome] MONEY NULL, 
    [Tier] NVARCHAR(128) NULL, 
    [Locality] NVARCHAR(128) NULL, 

    CONSTRAINT [PK_GovernmentPrograms] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO
EXEC sp_addextendedproperty N'MS_Description', N'Lookup table for GovernmentPrograms', 'SCHEMA', N'dbo', 'TABLE', N'GovernmentPrograms', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'i.e. USA - Illinois - Cook - Chicago', 'SCHEMA', N'dbo', 'TABLE', N'GovernmentPrograms', 'COLUMN', N'Locality'
GO
EXEC sp_addextendedproperty N'MS_Description', N'i.e. City, County, State, Federal', 'SCHEMA', N'dbo', 'TABLE', N'GovernmentPrograms', 'COLUMN', N'Tier'
