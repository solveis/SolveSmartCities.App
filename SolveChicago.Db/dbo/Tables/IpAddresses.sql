CREATE TABLE [dbo].[IpAddresses]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY, 
    [Address] NCHAR(15) NOT NULL
)
GO
EXEC sp_addextendedproperty N'MS_Description', N'Lookup table for IP Addresses', 'SCHEMA', N'dbo', 'TABLE', N'IpAddresses', NULL, NULL
