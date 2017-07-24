CREATE TABLE [dbo].[Ethnicities]
(
	[Id] INT IDENTITY(1, 1) PRIMARY KEY, 
    [EthnicityName] NVARCHAR(50) NOT NULL
)
GO
EXEC sp_addextendedproperty N'MS_Description', N'Lookup table for Ethnicities', 'SCHEMA', N'dbo', 'TABLE', N'Ethnicities', NULL, NULL
