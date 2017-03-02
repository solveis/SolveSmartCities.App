CREATE TABLE [dbo].[Members]
(
	[Id] INT IDENTITY(1, 1) NOT NULL,
	[Email] NVARCHAR(128) NOT NULL,
    [FirstName] NVARCHAR(128) NULL, 
    [LastName] NVARCHAR(128) NULL, 
    [ProfilePicturePath] NVARCHAR(MAX) NULL, 
	[Phone] NVARCHAR(128) NULL, 
    [CreatedDate] DATETIME2 NULL, 
    [FamilyId] INT NULL, 
    [FamilyRole] NVARCHAR(50) NULL, 
    [Gender] NVARCHAR(50) NULL, 
    [Birthday] DATETIME2 NULL, 
    [HighestEducation] NVARCHAR(128) NULL, 
    [LastSchool] NVARCHAR(MAX) NULL, 
    [Degree] NVARCHAR(128) NULL, 
    CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Members_Families] FOREIGN KEY (FamilyId) REFERENCES [Families](Id) ON DELETE CASCADE, 
)
