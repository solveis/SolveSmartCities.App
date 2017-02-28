CREATE TABLE [dbo].[UserProfileAdmins]
(
	[UserId] NVARCHAR(128)  NOT NULL, 
    [AdminId] INT NOT NULL,

	PRIMARY KEY ([AdminId], [UserId]), 
    CONSTRAINT [FK_UserProfileAdmins_AspNetUser] FOREIGN KEY (UserId) REFERENCES [AspNetUsers](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_UserProfileAdmins_Admin] FOREIGN KEY (AdminId) REFERENCES [Admins]([Id]) ON DELETE CASCADE
)
