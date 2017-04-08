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
