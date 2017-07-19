CREATE TABLE [dbo].[Addresses]
(
	[Id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY ,
    [Address1] NVARCHAR(128) NULL, 
    [Address2] NVARCHAR(50) NULL, 
    [City] NVARCHAR(50) NULL, 
    [Province] NVARCHAR(50) NULL, 
    [Country] NVARCHAR(50) NULL, 
	[ZipCode] NVARCHAR(10) NULL,
)

GO
EXEC sp_addextendedproperty N'MS_Description', N'Stores address information', 'SCHEMA', N'dbo', 'TABLE', N'Addresses', NULL, NULL
