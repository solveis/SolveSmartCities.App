CREATE TABLE [dbo].[UserProfileNonprofits]
(
	[UserId] INT NOT NULL, 
    [NonprofitId] INT NOT NULL,

	PRIMARY KEY ([NonprofitId], [UserId]), 
    CONSTRAINT [FK_UserProfileNonprofits_UserProfile] FOREIGN KEY (UserId) REFERENCES [UserProfile](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_UserProfileNonprofits_Nonprofit] FOREIGN KEY (NonprofitId) REFERENCES [Nonprofits]([Id]) ON DELETE CASCADE
)
