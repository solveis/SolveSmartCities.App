CREATE TABLE [dbo].[UserProfileCaseManagers]
(
	[UserId] NVARCHAR(128)  NOT NULL, 
    [CaseManagerId] INT NOT NULL,

	PRIMARY KEY ([CaseManagerId], [UserId]), 
    CONSTRAINT [FK_UserProfileCaseManagers_AspNetUser] FOREIGN KEY (UserId) REFERENCES [AspNetUsers](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_UserProfileCaseManagers_Nonprofit] FOREIGN KEY (CaseManagerId) REFERENCES [CaseManagers]([Id]) ON DELETE CASCADE
)
