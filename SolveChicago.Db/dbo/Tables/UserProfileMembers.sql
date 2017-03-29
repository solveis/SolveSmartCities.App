CREATE TABLE [dbo].[UserProfileMembers]
(
	[UserId] NVARCHAR(128)  NOT NULL, 
    [MemberId] INT NOT NULL,

	PRIMARY KEY ([MemberId], [UserId]), 
    CONSTRAINT [FK_UserProfileMembers_AspNetUser] FOREIGN KEY (UserId) REFERENCES [AspNetUsers](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_UserProfileMembers_Member] FOREIGN KEY (MemberId) REFERENCES [Members]([Id]) ON DELETE CASCADE
)
