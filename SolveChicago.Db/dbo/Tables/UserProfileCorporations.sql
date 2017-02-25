CREATE TABLE [dbo].[AspNetUserCorporations]
(
	[UserId] NVARCHAR(128)  NOT NULL, 
    [CorporationId] INT NOT NULL,

	PRIMARY KEY ([CorporationId], [UserId]), 
    CONSTRAINT [FK_AspNetUserCorporations_AspNetUser] FOREIGN KEY (UserId) REFERENCES [AspNetUser](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_AspNetUserCorporations_Corporation] FOREIGN KEY (CorporationId) REFERENCES [Corporations]([Id]) ON DELETE CASCADE
)
