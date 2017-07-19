CREATE TABLE [dbo].[Families]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
    [FamilyName] NVARCHAR(128) NULL,
    [CreatedDate] DATETIME2 NULL, 

    CONSTRAINT [PK_Families] PRIMARY KEY CLUSTERED ([Id] ASC),
)
GO
EXEC sp_addextendedproperty N'MS_Description', N'Stores information about Families', 'SCHEMA', N'dbo', 'TABLE', N'Families', NULL, NULL
