CREATE TABLE [dbo].[UserProfilePropertyManagers]
(
	[UserId] NVARCHAR(128)  NOT NULL, 
    [PropertyManagerId] INT NOT NULL,

	PRIMARY KEY ([PropertyManagerId], [UserId]), 
    CONSTRAINT [FK_UserProfilePropertyManagers_AspNetUser] FOREIGN KEY (UserId) REFERENCES [AspNetUsers](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_UserProfilePropertyManagers_PropertyManager] FOREIGN KEY (PropertyManagerId) REFERENCES [PropertyManagers]([Id]) ON DELETE CASCADE
)
