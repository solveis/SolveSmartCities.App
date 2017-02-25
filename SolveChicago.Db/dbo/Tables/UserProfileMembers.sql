CREATE TABLE [dbo].[AspNetUserMembers]
(
	[UserId] NVARCHAR(128)  NOT NULL, 
    [MemberId] INT NOT NULL,

	PRIMARY KEY ([MemberId], [UserId]), 
    CONSTRAINT [FK_AspNetUserMembers_AspNetUser] FOREIGN KEY (UserId) REFERENCES [AspNetUser](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_AspNetUserMembers_Member] FOREIGN KEY (MemberId) REFERENCES [Members]([Id]) ON DELETE CASCADE
)
