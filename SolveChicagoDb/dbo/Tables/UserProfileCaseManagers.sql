CREATE TABLE [dbo].[UserProfileCaseManagers]
(
	[UserId] INT NOT NULL, 
    [CaseManagerId] INT NOT NULL,

	PRIMARY KEY ([CaseManagerId], [UserId]), 
    CONSTRAINT [FK_UserProfileCaseManagers_UserProfile] FOREIGN KEY (UserId) REFERENCES [UserProfile](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_UserProfileCaseManagers_Nonprofit] FOREIGN KEY (CaseManagerId) REFERENCES [CaseManagers]([Id]) ON DELETE CASCADE
)
