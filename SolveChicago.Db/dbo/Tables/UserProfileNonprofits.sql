CREATE TABLE [dbo].[AspNetUserNonprofits]
(
	[UserId] NVARCHAR(128)  NOT NULL, 
    [NonprofitId] INT NOT NULL,

	PRIMARY KEY ([NonprofitId], [UserId]), 
    CONSTRAINT [FK_AspNetUserNonprofits_AspNetUser] FOREIGN KEY (UserId) REFERENCES [AspNetUser](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_AspNetUserNonprofits_Nonprofit] FOREIGN KEY (NonprofitId) REFERENCES [Nonprofits]([Id]) ON DELETE CASCADE
)
