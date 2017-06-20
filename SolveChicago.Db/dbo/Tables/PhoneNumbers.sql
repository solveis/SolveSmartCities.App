CREATE TABLE [dbo].[PhoneNumbers]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY, 
    [Number] NVARCHAR(10) NOT NULL, 
    [Extension] NVARCHAR(10) NULL
)
