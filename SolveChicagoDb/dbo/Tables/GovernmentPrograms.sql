CREATE TABLE [dbo].[GovernmentPrograms]
(
	[Id] INT IDENTITY(1, 1) NOT NULL, 
    [Name] NVARCHAR(MAX) NOT NULL,

	CONSTRAINT [PK_GovernmentPrograms] PRIMARY KEY CLUSTERED ([Id] ASC)
)
