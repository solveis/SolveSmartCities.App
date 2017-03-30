CREATE TABLE [dbo].[Communications]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
	[OrganizationId] INT NOT NULL, 
    [Type] NVARCHAR(256) NULL, 
    [Date] DATETIME2 NULL, 
    [UserId] NVARCHAR(128) NULL, 
    [Success] BIT NULL, 
    CONSTRAINT [PK_Communications] PRIMARY KEY (Id), 
	CONSTRAINT [FK_Communications_AspNetUsers] FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE
)
