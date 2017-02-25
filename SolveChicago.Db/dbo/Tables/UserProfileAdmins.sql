CREATE TABLE [dbo].[AspNetUserAdmins]
(
	[UserId] NVARCHAR(128)  NOT NULL, 
    [AdminId] INT NOT NULL,

	PRIMARY KEY ([AdminId], [UserId]), 
    CONSTRAINT [FK_AspNetUserAdmins_AspNetUser] FOREIGN KEY (UserId) REFERENCES [AspNetUser](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_AspNetUserAdmins_Admin] FOREIGN KEY (AdminId) REFERENCES [Admins]([Id]) ON DELETE CASCADE
)
