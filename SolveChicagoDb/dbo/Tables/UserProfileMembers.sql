CREATE TABLE [dbo].[UserProfileMembers]
(
	[UserId] INT NOT NULL, 
    [MemberId] INT NOT NULL,

	PRIMARY KEY ([MemberId], [UserId]), 
    CONSTRAINT [FK_UserProfileMembers_UserProfile] FOREIGN KEY (UserId) REFERENCES [UserProfile](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_UserProfileMembers_Member] FOREIGN KEY (MemberId) REFERENCES [Members]([Id]) ON DELETE CASCADE
)
