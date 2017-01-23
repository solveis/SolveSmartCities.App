CREATE TABLE [dbo].[UserProfileCorporations]
(
	[UserId] INT NOT NULL, 
    [CorporationId] INT NOT NULL,

	PRIMARY KEY ([CorporationId], [UserId]), 
    CONSTRAINT [FK_UserProfileCorporations_UserProfile] FOREIGN KEY (UserId) REFERENCES [UserProfile](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_UserProfileCorporations_Corporation] FOREIGN KEY (CorporationId) REFERENCES [Corporations]([Id]) ON DELETE CASCADE
)
