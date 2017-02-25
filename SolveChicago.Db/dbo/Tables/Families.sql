CREATE TABLE [dbo].[Families]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
    [FamilyName] NVARCHAR(128) NULL, 
    [HeadOfHousehold] INT NULL,
	[Phone] NVARCHAR(128) NULL, 
    [Address1] NVARCHAR(128) NULL, 
    [Address2] NVARCHAR(50) NULL, 
    [City] NVARCHAR(50) NULL, 
    [Province] NVARCHAR(50) NULL, 
    [Country] NVARCHAR(50) NULL, 
    [CreatedDate] DATETIME2 NULL, 

    CONSTRAINT [PK_Families] PRIMARY KEY CLUSTERED ([Id] ASC)
)
