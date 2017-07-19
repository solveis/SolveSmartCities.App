CREATE TABLE [dbo].[PhoneNumbers]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY, 
    [Number] NVARCHAR(10) NOT NULL, 
    [Extension] NVARCHAR(10) NULL
)
GO
EXEC sp_addextendedproperty N'MS_Description', N'Lookup table for PhoneNumbers', 'SCHEMA', N'dbo', 'TABLE', N'PhoneNumbers', NULL, NULL