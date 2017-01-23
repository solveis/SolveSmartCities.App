CREATE TABLE [dbo].[UserProfileAdmins]
(
	[UserId] INT NOT NULL, 
    [AdminId] INT NOT NULL,

	PRIMARY KEY ([AdminId], [UserId]), 
    CONSTRAINT [FK_UserProfileAdmins_UserProfile] FOREIGN KEY (UserId) REFERENCES [UserProfile](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_UserProfileAdmins_Admin] FOREIGN KEY (AdminId) REFERENCES [Admins]([Id]) ON DELETE CASCADE
)
