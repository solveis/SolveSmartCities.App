CREATE TABLE [dbo].[AspNetUserCaseManagers]
(
	[UserId] NVARCHAR(128)  NOT NULL, 
    [CaseManagerId] INT NOT NULL,

	PRIMARY KEY ([CaseManagerId], [UserId]), 
    CONSTRAINT [FK_AspNetUserCaseManagers_AspNetUser] FOREIGN KEY (UserId) REFERENCES [AspNetUser](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_AspNetUserCaseManagers_Nonprofit] FOREIGN KEY (CaseManagerId) REFERENCES [CaseManagers]([Id]) ON DELETE CASCADE
)
