CREATE TABLE [dbo].[CaseManagers]
(
	[Id] INT IDENTITY(1, 1) NOT NULL,
    [Email] NVARCHAR(128) NOT NULL, 
	[FirstName] NVARCHAR(128) NULL, 
	[LastName] NVARCHAR(128) NULL, 
    [ProfilePicturePath] NVARCHAR(MAX) NULL, 
	[Phone] NVARCHAR(128) NULL, 
    [CreatedDate] DATETIME2 NULL, 
    [NonprofitId] INT NULL, 

    [UserId] NVARCHAR(128) NOT NULL, 
    CONSTRAINT [PK_CaseManagers] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_CaseManagers_Nonprofit] FOREIGN KEY (NonprofitId) REFERENCES [Nonprofits](Id) ON DELETE SET NULL, 
    CONSTRAINT [FK_CaseManagers_AspNetUser] FOREIGN KEY (UserId) REFERENCES [AspNetUsers](Id) ON DELETE CASCADE, 
)
