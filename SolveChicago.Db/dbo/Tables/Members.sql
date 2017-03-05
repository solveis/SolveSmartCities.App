CREATE TABLE [dbo].[Members]
(
	[Id] INT IDENTITY(1, 1) NOT NULL,
	[Email] NVARCHAR(128) NOT NULL,
    [FirstName] NVARCHAR(128) NULL, 
    [LastName] NVARCHAR(128) NULL, 
    [ProfilePicturePath] NVARCHAR(MAX) NULL, 
    [CreatedDate] DATETIME2 NULL, 
    [Gender] NVARCHAR(50) NULL, 
    [Birthday] DATETIME2 NULL, 
    [FamilyId] INT NULL, 
    [IsHeadOfHousehold] BIT NULL, 
    CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Member_Family] FOREIGN KEY (FamilyId) REFERENCES [Families]([Id]) ON DELETE CASCADE,
)
